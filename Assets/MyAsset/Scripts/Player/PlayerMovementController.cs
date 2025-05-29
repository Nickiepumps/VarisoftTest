using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    [SerializeField] private InputActionReference inputMappingReference;
    public Vector2 direction { get; private set; }

    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }
    void FixedUpdate()
    {
        MoveJoyStick(); // For joystick
    }
    private void MoveJoyStick()
    {
        Vector2 inputVector = inputMappingReference.action.ReadValue<Vector2>(); // Read Vector2 from left joystick
        direction = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = direction * movementSpeed;
        Vector2 newPos = rbody.position + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }
}
