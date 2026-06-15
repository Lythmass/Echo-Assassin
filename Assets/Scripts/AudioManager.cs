using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Background Music")]
    [SerializeField]
    AudioClip backgroundMusic;

    [SerializeField]
    [Range(0f, 1f)]
    float musicVolume;

    [Header("Teleportation SFX")]
    [SerializeField]
    AudioClip[] teleportationSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float teleportationVolume;

    [Header("Dagger Hit SFX")]
    [SerializeField]
    AudioClip[] daggerHitSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float daggerHitVolume;

    [Header("Dagger Shoot SFX")]
    [SerializeField]
    AudioClip[] daggerShootSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float daggerShootVolume;

    [Header("Jump SFX")]
    [SerializeField]
    AudioClip[] jumpSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float jumpVolume;

    [Header("Land SFX")]
    [SerializeField]
    AudioClip[] landSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float landVolume;

    [Header("Heavy Land SFX")]
    [SerializeField]
    AudioClip[] heavyLandSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float heavyLandVolume;

    AudioSource audioSource;
    AudioSource musicSource;

    void Awake()
    {
        if (instance != null)
        {
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
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 0f;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.spatialBlend = 0f;
        musicSource.volume = musicVolume;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayTeleportationSFX()
    {
        int index = Random.Range(0, teleportationSFX.Length);
        audioSource.PlayOneShot(teleportationSFX[index], teleportationVolume);
    }

    public void PlayDaggerHitSFX()
    {
        int index = Random.Range(0, daggerHitSFX.Length);
        audioSource.PlayOneShot(daggerHitSFX[index], daggerHitVolume);
    }

    public void PlayDaggerShootSFX()
    {
        int index = Random.Range(0, daggerShootSFX.Length);
        audioSource.PlayOneShot(daggerShootSFX[index], daggerShootVolume);
    }

    public void PlayJumpSFX()
    {
        int index = Random.Range(0, jumpSFX.Length);
        audioSource.PlayOneShot(jumpSFX[index], jumpVolume);
    }

    public void PlayLandSFX()
    {
        int index = Random.Range(0, landSFX.Length);
        audioSource.PlayOneShot(landSFX[index], landVolume);
    }

    public void PlayHeavyLandSFX()
    {
        int index = Random.Range(0, heavyLandSFX.Length);
        audioSource.PlayOneShot(heavyLandSFX[index], heavyLandVolume);
    }
}
