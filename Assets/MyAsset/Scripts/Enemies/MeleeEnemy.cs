using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemie
{
    [SerializeField] private float chasingRange = 5f;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private CircleCollider2D damageTrigger;
    private bool isAttack = false;
    private void Update()
    {
        if (enemyHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        EnemySpriteDirection(player.transform);
        if (distanceToPlayer <= chasingRange && distanceToPlayer > 0.8f)
        {
            enemyAnimator.SetBool("IsAttack", false);
            enemyAnimator.SetBool("IsRun", true);
            Vector2 enemyMovement = Vector2.MoveTowards(transform.position, player.transform.position, walkSpeed * Time.fixedDeltaTime);
            enemyRB.MovePosition(enemyMovement);
        }
        else if(distanceToPlayer <= 0.8f && player.gameObject.activeSelf == true)
        {
            if(isAttack == false)
            {
                enemyAnimator.SetBool("IsAttack", true);
                enemyAnimator.SetBool("IsRun", false);
                StartCoroutine(EnemyMelee());
            }
        }
        else
        {
            enemyAnimator.SetBool("IsAttack", false);
            enemyAnimator.SetBool("IsRun", false);
        }
    }
    private IEnumerator EnemyMelee()
    {
        isAttack = true;
        // Play Attack anim
        yield return new WaitForSeconds(0.48f);
        damageTrigger.enabled = true;
        Debug.Log("Hit Player");
        yield return new WaitForSeconds(0.05f);
        damageTrigger.enabled = false;
        yield return new WaitForSeconds(0.13f);
        
        isAttack = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chasingRange);
    }
}
