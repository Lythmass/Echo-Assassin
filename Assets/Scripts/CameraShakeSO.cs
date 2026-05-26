using UnityEngine;

[CreateAssetMenu(fileName = "CameraShake", menuName = "Scriptable Objects/CameraShake")]
public class CameraShakeSO : ScriptableObject
{
    [SerializeField]
    float intensity;

    [SerializeField]
    float frequency;

    [SerializeField]
    float duration;

    public float GetIntensity()
    {
        return intensity;
    }

    public float GetFrequency()
    {
        return frequency;
    }

    public float GetDuration()
    {
        return duration;
    }
}
