using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    float slowTimeScale;

    [SerializeField]
    float timeResetDuration;

    public void SlowTimeDown()
    {
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ResetTime()
    {
        Time.timeScale += 1 / timeResetDuration * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
}
