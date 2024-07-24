using UnityEngine;

public class ShadeProjectile : MonoBehaviour
{
    public float speed = 2f; // Speed of the projectile
    public int damage = 1; // Damage dealt by the projectile
    private Vector2 direction; // Direction of the projectile

    private void Start()
    {
        // Destroy the projectile after 5 seconds to prevent memory leaks
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Move the projectile in the set direction
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerController player = collision.GetComponent<PlayerController>();
            WizardPlayerController wizardPlayer = collision.GetComponent<WizardPlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            else if (wizardPlayer != null)
            {
                wizardPlayer.TakeDamage(damage);
            }

            // Destroy the projectile upon collision
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            // Destroy the projectile upon hitting an obstacle
            Destroy(gameObject);
        }
    }
}
