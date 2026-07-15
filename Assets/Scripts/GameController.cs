using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    bool postProcessingEnabled = true;

    void Awake()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetPostProcessingEnabled(bool value)
    {
        postProcessingEnabled = value;
    }

    public bool GetPostProcessingEnabled()
    {
        return postProcessingEnabled;
    }
}
