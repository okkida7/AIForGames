using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isAttacking = false; // Tracks whether the player is attacking
    private bool attackRegistered = false; // Tracks whether the attack has been registered
    public bool isDead = false;
    public AudioSource hurtSE;
    public AudioSource deadSE;
    private bool canWalkThroughTrees = false;
    private bool canWalkThroughWater = false;
    private bool canWalkThroughOther = false;
    private bool increaseAttackPower = false;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(RegenerateStamina());
        CalculateScreenBounds();
    }

    void Update()
    {
        // Check Time.deltaTime to ensure the player cannot act during time scale = 0
        if (Time.deltaTime == 0 || isDead)
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

    void FixedUpdate()
    {
        // Clamp the player's position within the screen bounds
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
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
        hurtSE.Play();
        Invoke("ResetHurt", 0.3f);
        if (health <= 0)
        {
            isDead = true;
            hurtSE.Stop();
            deadSE.Play();
            if(health < 0)
            {
                deadSE.Stop();
            }
            animator.SetBool("isDead", true);
            Invoke("GameOver", 2f);
        }
    }

    public void TakeHealth(int healthAmount)
    {
        health += healthAmount;
        health = Mathf.Clamp(health, 0, 5);
    }

    void GameOver()
    {
        SceneManager.LoadScene("StartMenu");
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
            EnemyRangeAttack rangeEnemy = other.GetComponent<EnemyRangeAttack>();
            if (enemy != null && increaseAttackPower)
            {
                enemy.TakeDamage(2);
            }
            else if (enemy != null && !increaseAttackPower)
            {
                enemy.TakeDamage(1);
            }
            else if (rangeEnemy != null && increaseAttackPower)
            {
                rangeEnemy.TakeDamage(2);
            }
            else if (rangeEnemy != null && !increaseAttackPower)
            {
                rangeEnemy.TakeDamage(1);
            }
        }
        
    }

    public void EnableIncreaseAttackPower()
    {
        increaseAttackPower = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.tag);
        if (canWalkThroughTrees && collision.gameObject.CompareTag("Tree"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if(canWalkThroughWater && collision.gameObject.CompareTag("Water"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if(canWalkThroughOther && collision.gameObject.CompareTag("Obstacle"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    // Method to enable walking through trees
    public void EnableWalkThroughTrees()
    {
        canWalkThroughTrees = true;
    }

    public void EnableWalkThroughWater()
    {
        canWalkThroughWater = true;
    }

    public void EnableWalkThroughOther()
    {
        canWalkThroughOther = true;
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

    void CalculateScreenBounds()
    {
        Camera mainCamera = Camera.main;
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.z));

        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;
        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;
    }
}
