using System.Collections;
using System.Linq;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float teleportDelay;

    [SerializeField]
    CameraShakeSO cameraShakeSO;

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
        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        if (collision.gameObject.layer == platformLayerIndex)
        {
            CameraShake.Instance.ShakeCamera(
                cameraShakeSO.GetIntensity(),
                cameraShakeSO.GetFrequency(),
                cameraShakeSO.GetDuration()
            );
            hasCollided = true;
            StartCoroutine(TeleportPlayer());
        }
        else if (collision.gameObject.layer == enemyLayerIndex)
        {
            hasCollided = true;
            StartCoroutine(TeleportPlayer());
            Destroy(collision.gameObject, teleportDelay);
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
