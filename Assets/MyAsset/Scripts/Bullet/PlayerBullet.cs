using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Transform playerPos;
    private void OnEnable()
    {
        if(playerPos == null)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        gameObject.transform.position = playerPos.position;
    }
    private void FixedUpdate()
    {
        Vector2 bulletMovement = bulletDirection * travelSpeed * Time.fixedDeltaTime;
        Vector2 bulletPos = bulletRB.position + bulletMovement;
        bulletRB.MovePosition(bulletPos);

        // Disable bullet if it goes out of view
        Vector2 bulletWorldToViewportPos = Camera.main.WorldToViewportPoint(bulletPos);
        if(bulletWorldToViewportPos.x > 1 || bulletWorldToViewportPos.y > 1 || bulletWorldToViewportPos.x < 0 || bulletWorldToViewportPos.y < 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<Enemie>().enemyHealth -= bulletDamage;
            gameObject.SetActive(false);
        }
    }
}
