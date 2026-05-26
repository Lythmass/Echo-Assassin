using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float shakeTimer;
    float startingIntensity;
    float startingFrequency;
    float shakeTimerTotal;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public static CameraShake Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float frequency, float duration)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        startingIntensity = intensity;
        startingFrequency = frequency;
        shakeTimer = duration;
        shakeTimerTotal = duration;
    }

    void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            StopCameraShake();
        }
    }

    void StopCameraShake()
    {
        float time = 1 - (shakeTimer / shakeTimerTotal);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(
            startingIntensity,
            0f,
            time
        );
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = Mathf.Lerp(
            startingFrequency,
            0f,
            time
        );
    }
}
