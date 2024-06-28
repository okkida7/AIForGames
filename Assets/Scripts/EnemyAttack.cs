using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Collider2D attackCollider;
    public int damage = 1;
    Animator anim;
    public LayerMask playerLayer;
    public Transform leftAttackPoint;
    public Transform rightAttackPoint;
    public float attackRange = 0.5f;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void AttackRight() {
        attackCollider.enabled = true;
        Collider2D other = Physics2D.OverlapCircle(rightAttackPoint.position, attackRange, playerLayer);
        if(other == null) {
            return;
        }
        else if(other.tag == "Player"){
            PlayerController player = other.GetComponent<PlayerController>();
            if(player != null) {
                player.TakeDamage(damage);
            }
        }
    }

    public void AttackLeft() {
        attackCollider.enabled = true;
        Collider2D other = Physics2D.OverlapCircle(leftAttackPoint.position, attackRange, playerLayer);
        if(other == null) {
            return;
        }
        else if(other.tag == "Player"){
            PlayerController player = other.GetComponent<PlayerController>();
            if(player != null) {
                player.TakeDamage(damage);
            }
            WizardPlayerController wizardPlayer = other.GetComponent<WizardPlayerController>();
            if(wizardPlayer != null) {
                wizardPlayer.TakeDamage(damage);
            }
        }
    }

    public void StopAttack() {
        attackCollider.enabled = false;
    }


    public void OnDrawGizmosSelected(){
        if(rightAttackPoint == null || leftAttackPoint == null){
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftAttackPoint.position, attackRange);
        Gizmos.DrawWireSphere(rightAttackPoint.position, attackRange);
    }
}