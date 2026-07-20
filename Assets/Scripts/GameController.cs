using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    bool postProcessingEnabled = true;
    float audioVolume;

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

    void Start()
    {
        audioVolume = AudioManager.instance.GetComponent<AudioSource>().volume;
    }

    public void SetPostProcessingEnabled(bool value)
    {
        postProcessingEnabled = value;
    }

    public bool GetPostProcessingEnabled()
    {
        return postProcessingEnabled;
    }

    public float GetAudioVolume()
    {
        return audioVolume;
    }

    public void SetAudioVolume(float volume)
    {
        audioVolume = volume;
    }
}
