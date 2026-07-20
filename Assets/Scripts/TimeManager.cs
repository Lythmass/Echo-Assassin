using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    float slowTimeScale;

    [SerializeField]
    float timeResetDuration;

    [SerializeField]
    Pause pauseManager;

    public void SlowTimeDown()
    {
        if (pauseManager.GetIsPaused())
            return;
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ResetTime()
    {
        if (pauseManager.GetIsPaused())
            return;
        Time.timeScale += 1 / timeResetDuration * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
}
