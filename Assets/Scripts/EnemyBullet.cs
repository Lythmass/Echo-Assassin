using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    float bulletDestroyTime;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.up * bulletSpeed;
    }

    void Update()
    {
        Destroy(gameObject, bulletDestroyTime);
    }
}
