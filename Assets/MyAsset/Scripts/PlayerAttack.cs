using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAttack : MonoBehaviour
{
    private IsometricPlayerMovementController playerMovement;
    [SerializeField] private BulletPoolManager bulletPoolManager;
    [SerializeField] private InputActionAsset attckInputAction;
    private InputAction attackAction;
    [SerializeField] private Transform bulletSpawnPoint;

    // Arrays of bulletSpawnpoint euler angle.
    // Each index represents a direction the player can face start from 0 degree and goes on in counter-clockwise
    private Vector3[] bulletSpawnRotArr = new Vector3[] 
    {
        Vector3.zero,
        new Vector3(0, 0, 45),
        new Vector3(0, 0, 90),
        new Vector3(0, 0, 135),
        new Vector3(0, 0, 180),
        new Vector3(0, 0, -135),
        new Vector3(0, 0, -90),
        new Vector3(0, 0, -45)
    };
    private void OnEnable()
    {
        attckInputAction.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        attckInputAction.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        playerMovement = GetComponent<IsometricPlayerMovementController>();
        attackAction = attckInputAction.FindAction("Attack");
    }
    private void Update()
    {
        Aim(playerMovement.direction); // Aim player magic bullet to current direction the player is facing
        if (attackAction.WasPressedThisFrame())
        {
            Attack();
        }
    }
    private void Aim(Vector2 direction)
    {
        Vector2 normalDir = direction.normalized;
        float angle = Vector2.SignedAngle(Vector2.up, normalDir); // Get angle from joystick current direction
        float step = 360f / 8; // Get the angle when dividing by total amount of directions player can move (8 directions)
        float halfstep = step / 2; // Get half of the step angle
        angle += halfstep;

        // Make the angle positive
        if (angle < 0)
        {
            angle += 360;
        }
        int stepCount = Mathf.FloorToInt(angle / step); //calculate the amount of steps required to reach this angle

        if (direction.magnitude > 0.01f)
        {
            // Rotate the bullet spawn point based on the stepCount 
            switch (stepCount)
            {
                case 0:
                    bulletSpawnPoint.localRotation = Quaternion.Euler(bulletSpawnRotArr[0]);
                    break;
                case 1:
                    bulletSpawnPoint.localRotation = Quaternion.Euler(bulletSpawnRotArr[1]);
                    break;
                case 2:
                    bulletSpawnPoint.localRotation = Quaternion.Euler(bulletSpawnRotArr[2]);
                    break;
                case 3:
                    bulletSpawnPoint.localRotation = Quaternion.Euler(bulletSpawnRotArr[3]);
                    break;
                case 4:
                    bulletSpawnPoint.localRotation = Quaternion.Euler(bulletSpawnRotArr[4]);
                    break;
                case 5:
                    bulletSpawnPoint.localRotation = Quaternion.Euler(bulletSpawnRotArr[5]);
                    break;
                case 6:
                    bulletSpawnPoint.localRotation = Quaternion.Euler(bulletSpawnRotArr[6]);
                    break;
                case 7:
                    bulletSpawnPoint.localRotation = Quaternion.Euler(bulletSpawnRotArr[7]);
                    break;
            }
        }
    }
    private void Attack()
    {
        GameObject bullet = bulletPoolManager.CheckAvailableBullet();
        if(bullet != null)
        {
            bullet.transform.localRotation = bulletSpawnPoint.localRotation;
            bullet.GetComponent<Bullet>().direction = bulletSpawnPoint.up;
            bullet.SetActive(true);
        }
    }
}
