using System.Collections;
using System.Linq;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float teleportDelay;

    Rigidbody2D rb;
    bool hasCollided;
    PlayerShooting playerShooting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerShooting = FindAnyObjectByType<PlayerShooting>();
    }

    void FixedUpdate()
    {
        if (!hasCollided)
        {
            rb.linearVelocity = transform.up * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int platformLayerIndex = LayerMask.NameToLayer("Platform");
        if (collision.gameObject.layer == platformLayerIndex)
        {
            hasCollided = true;
            StartCoroutine(TeleportPlayer());
        }
    }

    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(teleportDelay);
        playerShooting.transform.position = transform.position;
        playerShooting.SetCanPlayerShoot(true);
        yield return null;
        Destroy(gameObject);
    }
}
