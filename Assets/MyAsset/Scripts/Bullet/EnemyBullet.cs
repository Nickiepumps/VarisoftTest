using UnityEngine;

public class EnemyBullet : Bullet
{
    private void FixedUpdate()
    {
        Vector2 bulletMovement = bulletDirection * travelSpeed * Time.fixedDeltaTime;
        Vector2 bulletPos = bulletRB.position + bulletMovement;
        bulletRB.MovePosition(bulletPos);

        // Disable bullet if it goes out of view
        Vector2 bulletWorldToViewportPos = Camera.main.WorldToViewportPoint(bulletPos);
        if (bulletWorldToViewportPos.x > 1 || bulletWorldToViewportPos.y > 1 || bulletWorldToViewportPos.x < 0 || bulletWorldToViewportPos.y < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().currentHealth -= bulletDamage;
            Destroy(gameObject);
        }
    }
}
