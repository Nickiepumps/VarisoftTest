using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private TMP_Text healthAmountText;
    public int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
        healthAmountText.text = "X" + currentHealth.ToString();
    }
    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp player health where min = 0 and max = maxHealth
        healthAmountText.text = "X" + currentHealth.ToString();
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            // Reduce the player health if collide with an enemy damage trigger
            currentHealth -= collision.GetComponent<Enemie>().enemyDamage;
        }
    }
}
