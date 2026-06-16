using UnityEngine;

public class NextLevelPortal : MonoBehaviour
{
    [SerializeField]
    LayerMask playerLayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("asdf");
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            LevelManager.instance.LoadNextLevel();
        }
    }
}
