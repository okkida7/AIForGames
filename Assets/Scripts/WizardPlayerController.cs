using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health = 5f; // Player health
    public float stamina = 5f;
    public float maxStamina = 5f;
    public float staminaRegenRate = 1f;
    public float staminaCost = 2f;
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint;
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
            Attack();
            stamina -= staminaCost;
            Invoke("StopAttacking", 0.3f); // Stop attacking after a short delay
        }
    }

    void Attack()
    {
        // Instantiate the projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        WizardProjectile projectileScript = projectile.GetComponent<WizardProjectile>();
        if (spriteRenderer.flipX)
        {
            projectileScript.SetDirection(Vector2.left);
        }
        else
        {
            projectileScript.SetDirection(Vector2.right);
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
        hurtSE.Play();
        Invoke("ResetHurt", 0.3f);
        if (health <= 0)
        {
            Die();
            if(health < 0)
            {
                deadSE.Stop();
            }
            Invoke("GameOver", 2f);
        }
    }

    public void TakeHealth(int healthAmount)
    {
        health += healthAmount;
        health = Mathf.Clamp(health, 0, 5);
    }

    void Die()
    {
        isDead = true;
        hurtSE.Stop();
        deadSE.Play();
        animator.SetBool("isDead", true);
    }

    void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }

    void ResetHurt()
    {
        hurtSE.Stop();
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
}
