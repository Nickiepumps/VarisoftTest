using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemie
{
    [SerializeField] private Transform enemyBulletPivot;
    [SerializeField] private Transform enemyBulletSpawner;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackSpeed = 3f;
    private float attackCooldown = 0;
    [SerializeField] private GameObject enemyBulletPrefab;
    private bool isAttack = false;
    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (enemyHealth <= 0)
        {
            StartCoroutine(EnemyDeadExplosion());
        }
        if (distanceToPlayer <= attackRange && player.activeSelf == true)
        {
            EnemySpriteDirection(player.transform);
            EnemyAim(); // Rotate the pivot toward the player;
            if(attackCooldown <= 0 && isAttack == false)
            {
                StartCoroutine(ShootPlayer());
            }
            else
            {
                attackCooldown -= Time.deltaTime;
            }
            
        }
        else
        {
            enemyAnimator.SetBool("IsAttack", false);
        }
    }
    private IEnumerator ShootPlayer()
    {
        isAttack = true;
        enemyAnimator.SetBool("IsAttack", true);
        yield return new WaitForSeconds(0.3f);
        GameObject enemyBullet = Instantiate(enemyBulletPrefab, enemyBulletSpawner.position, enemyBulletPivot.localRotation);
        enemyBullet.GetComponent<Bullet>().bulletDirection = enemyBulletPivot.up; // Set the bullet direction to the pivot's up direction (y axis)
        enemyBullet.GetComponent<Bullet>().bulletDamage = enemyDamage;
        enemyAnimator.SetBool("IsAttack", false);
        attackCooldown = attackSpeed;
        isAttack = false;
    }
    private void EnemyAim()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // rotate the pivot toward the player and offset to 90 degrees get y axis 
        enemyBulletPivot.localRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
