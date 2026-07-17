using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    float deathBlowUpForce = 15f;

    [SerializeField]
    float deathDelayTime = 1f;

    [SerializeField]
    float levelResetDelayTime = 1f;

    [SerializeField]
    LayerMask lethalLayers;

    [SerializeField]
    ParticleSystem deathParticles;

    Rigidbody2D rb;
    bool isAlive;
    Transform[] children;

    void Awake()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        children = GetComponentsInChildren<Transform>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleDamage(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        HandleDamage(collision.gameObject);
    }

    void HandleDamage(GameObject gameObject)
    {
        int collisionLayerMask = 1 << gameObject.layer;
        if ((collisionLayerMask & lethalLayers) != 0 && isAlive)
        {
            AudioManager.instance.PlayPlayerDamageSFX();
            isAlive = false;
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(WaitBeforeLevelReset());
        }
    }

    IEnumerator WaitBeforeLevelReset()
    {
        rb.linearVelocityY = deathBlowUpForce;
        yield return new WaitForSeconds(deathDelayTime);

        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        AudioManager.instance.PlayPlayerBlowUpSFX();

        Instantiate(deathParticles, transform.position, quaternion.identity);
        foreach (Transform child in children)
        {
            if (child != transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(levelResetDelayTime);
        LevelManager.instance.ResetCurrentLevel();
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }
}
