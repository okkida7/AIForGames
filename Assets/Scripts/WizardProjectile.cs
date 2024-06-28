using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;
    private WizardPlayerController wizardPlayer;
    private Vector2 direction;


    public void SetDirection(Vector2 newDirection){
        direction = newDirection.normalized;
    }

    private void Start()
    {
        // Destroy the projectile after 5 seconds to prevent memory leaks
        wizardPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<WizardPlayerController>();
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Move the projectile forward
        if(wizardPlayer != null && wizardPlayer.isDead == false)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            EnemyBehavior enemy = collision.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
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
