using System.Collections;
using UnityEngine;

public class NextLevelPortal : MonoBehaviour
{
    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    Color normalColor;

    [SerializeField]
    Color grayColor;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    ParticleSystem particles;

    [SerializeField]
    float levelChangeDelay = 0.5f;

    [SerializeField]
    float floatAmplitude = 4f;

    [SerializeField]
    float floatSpeed = 0.25f;

    [SerializeField]
    float timeBetweenNewPoints = 0.25f;
    bool hasKey = false;
    Vector2 startingPosition;
    Vector2 direction;
    float currentTimeBetweenNewPoints;

    void Start()
    {
        spriteRenderer.color = grayColor;
        startingPosition = transform.position;
        currentTimeBetweenNewPoints = timeBetweenNewPoints;
    }

    void Update()
    {
        if (hasKey)
        {
            FloatPortal();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasKey && ((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            Invoke("GoToNextLevel", levelChangeDelay);
        }
    }

    void GoToNextLevel()
    {
        LevelManager.instance.LoadNextLevel();
    }

    void FloatPortal()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            startingPosition + direction,
            floatSpeed * Time.deltaTime
        );
        if (currentTimeBetweenNewPoints < 0f)
        {
            direction = Random.insideUnitCircle * floatAmplitude;
            currentTimeBetweenNewPoints = timeBetweenNewPoints;
        }
        else
        {
            currentTimeBetweenNewPoints -= Time.deltaTime;
        }
    }

    public void HasPickedUpKey()
    {
        hasKey = true;
        spriteRenderer.color = normalColor;
        particles.Play();
    }
}
