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

    [SerializeField]
    ParticleSystem teleportDisplaceParticles;

    [SerializeField]
    ParticleSystem preTeleportParticles;

    Rigidbody2D rb;
    bool hasCollided;
    PlayerShooting playerShooting;
    PlayerController playerController;
    float lifetime = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerShooting = FindAnyObjectByType<PlayerShooting>();
        playerController = FindAnyObjectByType<PlayerController>();
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
            Instantiate(
                daggerHitParticles,
                teleportPosition.position,
                Quaternion.AngleAxis(transform.eulerAngles.z + 225, Vector3.forward)
            );
            AudioManager.instance.PlayDaggerHitSFX();
            hasCollided = true;
            StartCoroutine(TeleportPlayer());
        }
        else if (collision.gameObject.layer == enemyLayerIndex)
        {
            hasCollided = true;
            StartCoroutine(TeleportPlayer());
            collision.gameObject.GetComponent<EnemyController>().StopEnemyShootingCoroutine();
            Destroy(collision.gameObject, teleportDelay);
        }
    }

    IEnumerator TeleportPlayer()
    {
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        AudioManager.instance.PlayTeleportationSFX();
        yield return new WaitForSeconds(teleportDelay);
        playerController.ResetVelocity();
        Instantiate(preTeleportParticles, playerController.transform.position, Quaternion.identity);
        playerShooting.transform.position = teleportPosition.position;
        Instantiate(
            teleportDisplaceParticles,
            playerController.transform.position,
            Quaternion.identity
        );
        int direction = (int)-Mathf.Sign(Mathf.DeltaAngle(0, transform.eulerAngles.z));
        playerController.SetCameraHorizontalOffset(direction);
        playerShooting.transform.localScale = new Vector2(
            direction,
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
