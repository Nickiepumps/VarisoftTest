using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D bulletRB;
    public float travelSpeed;
    public int bulletDamage = 1;
    [HideInInspector] public Vector2 bulletDirection;
}
