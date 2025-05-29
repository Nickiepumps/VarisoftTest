using System.Collections;
using UnityEngine;

public class FlyingEnemy : Enemie
{
    [SerializeField] private float movementSpeed = 1f;
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
        if(distanceToPlayer <= 0.5f && player.gameObject.activeSelf == true)
        {
            enemyAnimator.SetBool("IsAttack", true);
            if (isAttack == false)
            {
                // Start attacking the player when get too close
                StartCoroutine(FlyingEnemyAttack());
            }   
        }
        else
        {
            // Continue chasing the player if the player is not too close 
            enemyAnimator.SetBool("IsAttack", false);
            Vector2 direction = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.fixedDeltaTime);
            enemyRB.MovePosition(direction);
        }
    }
    private IEnumerator FlyingEnemyAttack()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.7f);
        damageTrigger.enabled = true;
        yield return new WaitForSeconds(0.05f);
        damageTrigger.enabled = false;
        isAttack = false;
    }
}
