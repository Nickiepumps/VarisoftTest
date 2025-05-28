using System.Collections;
using System.Collections.Generic;
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
            gameObject.SetActive(false);
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
                StartCoroutine(FlyingEnemyAttack());
            }   
        }
        else
        {
            enemyAnimator.SetBool("IsAttack", false);
            Vector2 direction = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.fixedDeltaTime);
            enemyRB.MovePosition(direction);
        }
    }
    private IEnumerator FlyingEnemyAttack()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Flying Enemy Hit");
        damageTrigger.enabled = true;
        yield return new WaitForSeconds(0.05f);
        damageTrigger.enabled = false;
        isAttack = false;
    }
}
