using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health = 5f; // Player health
    public float stamina = 5f;
    public float maxStamina = 5f;
    public float staminaRegenRate = 1f;
    public float staminaCost = 2f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isAttacking = false; // Tracks whether the player is attacking
    private bool attackRegistered = false; // Tracks whether the attack has been registered

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(RegenerateStamina());
    }

    void Update()
    {
        // Check Time.deltaTime to ensure the player cannot act during time scale = 0
        if (Time.deltaTime == 0)
        {
            return;
        }
        // Capture input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateAnimation();

        // Set the velocity of the Rigidbody2D to move the player
        rb.velocity = movement.normalized * moveSpeed;

        // Handle attack input
        if (Input.GetKeyDown(KeyCode.Space) && stamina >= staminaCost)
        {
            animator.SetBool("isAttacking", true);
            isAttacking = true;
            attackRegistered = false; // Reset attack registration
            stamina -= staminaCost;
            Invoke("StopAttacking", 0.3f); // Stop attacking after a short delay
        }
    }

    void UpdateAnimation()
    {
        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            if (movement.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movement.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void StopAttacking()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetBool("isHurt", true);
        Invoke("ResetHurt", 0.3f);
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            Destroy(gameObject, 2f);
        }
    }

    void ResetHurt()
    {
        animator.SetBool("isHurt", false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && isAttacking && !attackRegistered)
        {
            attackRegistered = true;
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            enemy.TakeDamage(1);
        }
        
    }

    IEnumerator RegenerateStamina()
    {
        while (true)
        {
            if (!isAttacking && stamina < maxStamina)
            {
                stamina += staminaRegenRate * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0, maxStamina);
            }
            yield return null;
        }
    }

    public float Health {
        set {
            health = value;
        }
        get {
            return health;
        }
    }
}
