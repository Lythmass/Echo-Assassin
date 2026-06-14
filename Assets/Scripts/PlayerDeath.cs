using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    LayerMask hazardsLayer;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & hazardsLayer) != 0)
        {
            LevelManager.instance.ResetCurrentLevel();
        }
    }
}
