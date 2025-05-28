using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public Rigidbody2D enemyRB;
    [SerializeField] public Animator enemyAnimator;
    public int enemyHealth = 3;
    public int enemyDamage = 3;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        enemyRB = GetComponent<Rigidbody2D>();
    }
    public void EnemySpriteDirection(Transform player)
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Find the angle between the enemy and the player
        enemyAnimator.SetFloat("Angle", angle);
    }
}
