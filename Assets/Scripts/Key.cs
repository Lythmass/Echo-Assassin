using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    float destroyDelay = 0.25f;

    [SerializeField]
    ParticleSystem collectionParticles;
    NextLevelPortal nextLevelPortal;

    void Awake()
    {
        nextLevelPortal = FindAnyObjectByType<NextLevelPortal>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            nextLevelPortal.HasPickedUpKey();
            AudioManager.instance.PlayKeyCollectionSFX();
            Instantiate(collectionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject, destroyDelay);
        }
    }
}
