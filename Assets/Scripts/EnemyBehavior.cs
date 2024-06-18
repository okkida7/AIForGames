using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 3;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetBool("isHit", true);
        Invoke("ResetHit", 0.3f);
        if (health <= 0)
        {
            Die();
        }
    }

    void ResetHit()
    {
        animator.SetBool("isHit", false);
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        Destroy(gameObject, 1f);
    }
}
