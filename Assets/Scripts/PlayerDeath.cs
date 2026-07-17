using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    LayerMask lethalLayers;

    void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayerMask = 1 << collision.gameObject.layer;
        HandleDamage(collisionLayerMask);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int collisionLayerMask = 1 << collision.gameObject.layer;
        HandleDamage(collisionLayerMask);
    }

    void HandleDamage(int collisionLayerMask)
    {
        if ((collisionLayerMask & lethalLayers) != 0)
        {
            LevelManager.instance.ResetCurrentLevel();
        }
    }
}
