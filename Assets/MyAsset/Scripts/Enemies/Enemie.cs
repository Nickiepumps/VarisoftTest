using System.Collections;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public Rigidbody2D enemyRB;
    [SerializeField] public Animator enemyAnimator;
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private CircleCollider2D enemyCollider;
    [SerializeField] private GameObject explosionFX;
    public int enemyHealth = 3;
    public int enemyDamage = 3;
    private void OnEnable()
    {
        explosionFX.transform.parent = transform;
        enemySpriteRenderer.enabled = true;
        enemyCollider.enabled = true;
    }
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
    public IEnumerator EnemyDeadExplosion()
    {
        enemySpriteRenderer.enabled = false;
        enemyCollider.enabled = false;
        explosionFX.SetActive(true);
        yield return new WaitForSeconds(0.55f);
        explosionFX.SetActive(false);
        gameObject.SetActive(false);
    }
}
