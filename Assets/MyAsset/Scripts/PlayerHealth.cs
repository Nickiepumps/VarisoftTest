using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    public int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            currentHealth -= collision.GetComponent<Enemie>().enemyDamage;
        }
    }
}
