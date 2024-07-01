using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    public int health = 3; // Health of the enemy
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // Point from where the projectile is instantiated
    public float attackRadius = 10f; // Radius within which the enemy can attack
    public float attackCooldown = 2f; // Time between attacks
    public float animTime;
    public float setAnimTime;
    public float cd;
    public float setCD;
    public int damage = 1;
    public SpriteRenderer spriteRenderer; 

    private Animator anim;
    private bool isAttacking = false;

    private float nextAttackTime = 0f;
    private Transform player;

    public void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Time.deltaTime == 0)
        {
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null && Time.time >= nextAttackTime)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRadius)
            {
                spriteRenderer.flipX = player.position.x > transform.position.x;
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            } 
        } 
    }


    void Attack()
    {
        if(animTime>= 0){
            if(cd <= 0.1f){
                isAttacking = true;
                anim.SetBool("isAttacking", isAttacking);
                animTime -= Time.deltaTime;
                cd = setCD;
            } else{
                isAttacking = false;
                anim.SetBool("isAttacking", isAttacking);
                cd -= Time.deltaTime;
            } if(animTime < setAnimTime){
                animTime -= Time.deltaTime;
            }
        }
        else{
            animTime = setAnimTime;
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        ShadeProjectile projectileScript = projectile.GetComponent<ShadeProjectile>();
        Vector2 direction = (player.position - firePoint.position).normalized;
        projectileScript.SetDirection(direction);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetBool("isHit", true);
        Invoke("ResetHurt", 0.3f);
        if (health <= 0)
        {
            Die();
        }
    }

    void ResetHurt()
    {
        anim.SetBool("isHit", false);
    }

    void Die()
    {
        anim.SetBool("isDead", true);
        // enemy cannot move after death
        spriteRenderer.flipX = false;
        Destroy(gameObject, 1f);
    }

    void OnDrawGizmosSelected()
    {
        // Draw the attack radius in the editor for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
