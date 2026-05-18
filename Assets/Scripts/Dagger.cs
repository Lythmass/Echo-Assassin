using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float destroyTimer;

    Rigidbody2D rb;
    bool hasCollided;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!hasCollided)
        {
            rb.linearVelocity = transform.up * speed;
        }
        Destroy(gameObject, destroyTimer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int platformLayerIndex = LayerMask.NameToLayer("Platform");
        if (collision.gameObject.layer == platformLayerIndex)
        {
            hasCollided = true;
        }
    }
}
