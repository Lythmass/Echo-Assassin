using UnityEngine;

public class GlobalVolume : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(GameController.instance.GetPostProcessingEnabled());
    }
}
