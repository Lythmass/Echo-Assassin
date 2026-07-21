using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Enemy Death SFX")]
    [SerializeField]
    AudioClip[] enemyDeathSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float enemyDeathVolume;

    [Header("UI Hover SFX")]
    [SerializeField]
    AudioClip[] uiHoverSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float uiHoverVolume;

    [Header("UI Click SFX")]
    [SerializeField]
    AudioClip[] uiClickSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float uiClickVolume;

    [Header("Key Collection SFX")]
    [SerializeField]
    AudioClip[] keyCollectionSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float keyCollectionVolume;

    [Header("Enemy Shooting SFX")]
    [SerializeField]
    AudioClip[] enemyShootingSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float enemyShootingVolume;

    [Header("Enemy Bullet Hit SFX")]
    [SerializeField]
    AudioClip[] enemyBulletHitSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float enemyBulletHitVolume;

    [Header("Player Damage SFX")]
    [SerializeField]
    AudioClip[] playerDamageSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float playerDamageVolume;

    [Header("Player Blow Up SFX")]
    [SerializeField]
    AudioClip[] playerBlowUpSFX;

    [SerializeField]
    [Range(0f, 1f)]
    float playerBlowUpVolume;

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

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    void Update()
    {
        if (
            SceneManager.GetActiveScene().buildIndex == 0
            || SceneManager.GetActiveScene().buildIndex
                == SceneManager.sceneCountInBuildSettings - 1
        )
        {
            musicSource.pitch = 0.8f;
        }
        else
        {
            musicSource.pitch = 1f;
        }
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

    public void PlayEnemyDeathSFX()
    {
        int index = Random.Range(0, enemyDeathSFX.Length);
        audioSource.PlayOneShot(enemyDeathSFX[index], enemyDeathVolume);
    }

    public void PlayUIHoverSFX()
    {
        int index = Random.Range(0, uiHoverSFX.Length);
        audioSource.PlayOneShot(uiHoverSFX[index], uiHoverVolume);
    }

    public void PlayUIClickSFX()
    {
        int index = Random.Range(0, uiClickSFX.Length);
        audioSource.PlayOneShot(uiClickSFX[index], uiClickVolume);
    }

    public void PlayKeyCollectionSFX()
    {
        int index = Random.Range(0, keyCollectionSFX.Length);
        audioSource.PlayOneShot(keyCollectionSFX[index], keyCollectionVolume);
    }

    public void PlayEnemyShootingSFX()
    {
        int index = Random.Range(0, enemyShootingSFX.Length);
        audioSource.PlayOneShot(enemyShootingSFX[index], enemyShootingVolume);
    }

    public void PlayEnemyBulletHitSFX()
    {
        int index = Random.Range(0, enemyBulletHitSFX.Length);
        audioSource.PlayOneShot(enemyBulletHitSFX[index], enemyBulletHitVolume);
    }

    public void PlayPlayerDamageSFX()
    {
        int index = Random.Range(0, playerDamageSFX.Length);
        audioSource.PlayOneShot(playerDamageSFX[index], playerDamageVolume);
    }

    public void PlayPlayerBlowUpSFX()
    {
        int index = Random.Range(0, playerBlowUpSFX.Length);
        audioSource.PlayOneShot(playerBlowUpSFX[index], playerBlowUpVolume);
    }
}
