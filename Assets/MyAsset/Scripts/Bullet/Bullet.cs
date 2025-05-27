using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] private float travelSpeed;
    public Vector2 direction;
    private void OnEnable()
    {
        if(playerPos == null)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        gameObject.transform.position = playerPos.position;
    }
    private void OnDisable()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 bulletMovement = direction * travelSpeed * Time.fixedDeltaTime;
        Vector2 bulletPos = bulletRB.position + bulletMovement;
        bulletRB.MovePosition(bulletPos);
    }
}
