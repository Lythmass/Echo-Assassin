using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    float bulletDestroyTime;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    LayerMask obstaclesLayer;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        int collisionLayerMask = 1 << collision.gameObject.layer;
        if ((collisionLayerMask & playerLayer) != 0)
        {
            LevelManager.instance.ResetCurrentLevel();
        }

        if ((collisionLayerMask & obstaclesLayer) != 0)
        {
            Destroy(gameObject);
        }
    }
}
