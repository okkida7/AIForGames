using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 3;
    public float patrolSpeed = 1f;
    public float chasingSpeed = 2f;
    public float patrolWaitTime = 1f;
    private float waitTime;
    public float radius;
    public float attackRange = 1.5f;
    public float animTime;
    public float setAnimTime;
    public float Cd;
    public float setCD;
    public int damage = 1;

    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    private Transform playerTransform;
    public Transform attackPoint;
    
    private Animator anim;

    private bool isAttacking = false;

    public EnemyAttack enemyAttack;

    public SpriteRenderer spriteRenderer;

    public void Start()
    {
        waitTime = patrolWaitTime;
        movePos.position = GetRandomPos();
        anim = GetComponent<Animator>();
        //playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if(Time.deltaTime == 0)
        {
            return;
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance < radius)
            {
                if (Vector2.Distance(transform.position, playerTransform.position) < attackRange)
                {
                    spriteRenderer.flipX = playerTransform.position.x > transform.position.x;
                    Attacking();
                }
                else
                {
                    anim.SetBool("isAttacking", false);
                    GoToTarget();
                }
            }
            else
            {
                Patrol();
            }
        }
    }

    void Patrol()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, patrolSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                movePos.position = GetRandomPos();
                waitTime = patrolWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
                anim.SetBool("isWalking", false);
            }
        }
        if ((movePos.position.x - transform.position.x) > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if ((movePos.position.x - transform.position.x) < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }

    private void Attacking()
    {
        anim.SetBool("isWalking", false);
        if (animTime >= 0)
        {
            if (Cd <= 0.1f)
            {
                isAttacking = true;
                anim.SetBool("isAttacking", isAttacking);
                animTime -= Time.deltaTime;
                Cd = setCD;
            }
            else
            {
                isAttacking = false;
                anim.SetBool("isAttacking", isAttacking);
                Cd -= Time.deltaTime;
            }
            if (animTime < setAnimTime)
            {
                animTime -= Time.deltaTime;
            }
        }
        else
        {
            if(enemyAttack != null){
                enemyAttack.StopAttack();
            }
            animTime = setAnimTime;
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
        }
    }

    public void AnimationTrigger()
    {
        if(spriteRenderer.flipX == true)
        {
            enemyAttack.AttackRight();
        }
        else
        {
            enemyAttack.AttackLeft();
        }
    }

    private void GoToTarget()
    {
        anim.SetBool("isWalking", true);
        spriteRenderer.flipX = playerTransform.position.x > transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, chasingSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetBool("isHit", true);
        Invoke("ResetHit", 0.3f);
        if (health <= 0)
        {
            Die();
        }
    }

    void ResetHit()
    {
        anim.SetBool("isHit", false);
    }

    void Die()
    {
        anim.SetBool("isDead", true);
        // enemy cannot move after death
        patrolSpeed = 0;
        chasingSpeed = 0;
        spriteRenderer.flipX = false;
        Destroy(gameObject, 1f);
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, radius);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}