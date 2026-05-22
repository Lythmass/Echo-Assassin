using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float shakeDuration = 0.5f;

    [SerializeField]
    float shakeMagnitude = 0.5f;

    Vector3 position;

    void Awake()
    {
        position = transform.position;
    }

    public void SlamShake()
    {
        StartCoroutine(PlaySlamShake());
    }

    public void ShootShake()
    {
        StartCoroutine(PlayShootShake());
    }

    IEnumerator PlaySlamShake()
    {
        float timeElapsed = 0f;
        while (timeElapsed < shakeDuration)
        {
            transform.position =
                position + new Vector3(0, Random.insideUnitCircle.y * shakeMagnitude, 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = position;
    }

    IEnumerator PlayShootShake()
    {
        float timeElapsed = 0f;
        while (timeElapsed < shakeDuration)
        {
            transform.position = position + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = position;
    }
}
