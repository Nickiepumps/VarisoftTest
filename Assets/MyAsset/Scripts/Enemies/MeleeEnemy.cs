using System.Collections;
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
            // Enable explosion fx when hp = 0
            StartCoroutine(EnemyDeadExplosion());
        }
    }
    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        EnemySpriteDirection(player.transform);
        if (distanceToPlayer <= chasingRange && distanceToPlayer > 0.8f)
        {
            // Start chasing the player when reaching the chasing range
            enemyAnimator.SetBool("IsAttack", false);
            enemyAnimator.SetBool("IsRun", true);
            Vector2 enemyMovement = Vector2.MoveTowards(transform.position, player.transform.position, walkSpeed * Time.fixedDeltaTime);
            enemyRB.MovePosition(enemyMovement);
        }
        else if(distanceToPlayer <= 0.8f && player.gameObject.activeSelf == true)
        {
            // Attack the player when get too close
            if(isAttack == false)
            {
                enemyAnimator.SetBool("IsAttack", true);
                enemyAnimator.SetBool("IsRun", false);
                StartCoroutine(EnemyMelee()); // Enemy start attacking the player
            }
        }
        else
        {
            // Change to Idle
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
