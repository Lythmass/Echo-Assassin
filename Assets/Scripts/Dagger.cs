using System.Collections;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float teleportDelay;

    [SerializeField]
    CameraShakeSO cameraShakeSO;

    [SerializeField]
    Transform teleportPosition;

    [SerializeField]
    ParticleSystem daggerHitParticles;

    Rigidbody2D rb;
    bool hasCollided;
    PlayerShooting playerShooting;
    float lifetime = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerShooting = FindAnyObjectByType<PlayerShooting>();
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
            playerShooting.SetCanPlayerShoot(true);
        }
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
        int hazardsLayerIndex = LayerMask.NameToLayer("Hazards");
        if (
            collision.gameObject.layer == platformLayerIndex
            || collision.gameObject.layer == hazardsLayerIndex
        )
        {
            Instantiate(daggerHitParticles, teleportPosition.position, Quaternion.identity);
            AudioManager.instance.PlayDaggerHitSFX();
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
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        AudioManager.instance.PlayTeleportationSFX();
        yield return new WaitForSeconds(teleportDelay);
        playerShooting.transform.position = teleportPosition.position;
        playerShooting.transform.localScale = new Vector2(
            -Mathf.Sign(Mathf.DeltaAngle(0, transform.eulerAngles.z)),
            playerShooting.transform.localScale.y
        );
        CameraShake.Instance.ShakeCamera(
            cameraShakeSO.GetIntensity(),
            cameraShakeSO.GetFrequency(),
            cameraShakeSO.GetDuration()
        );

        playerShooting.SetCanPlayerShoot(true);
        yield return null;
        Destroy(gameObject);
    }
}
