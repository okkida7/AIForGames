using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int health = 5; // Player health
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
    }

    void Update()
    {
        // Capture input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateAnimation();

        // Set the velocity of the Rigidbody2D to move the player
        rb.velocity = movement.normalized * moveSpeed;

        // Handle attack input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isAttacking", true);
            isAttacking = true;
            attackRegistered = false; // Reset attack registration
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
        Debug.Log("Player Health: " + health);

        if (health <= 0)
        {
            Debug.Log("Player is dead!");
            // Handle player death (e.g., trigger death animation, restart level, etc.)
            // animator.SetTrigger("death");
        }
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
}
