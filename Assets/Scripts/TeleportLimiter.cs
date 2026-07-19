using UnityEngine;
using UnityEngine.UI;

public class TeleportLimiter : MonoBehaviour
{
    [SerializeField]
    int maxTeleportCount = 5;
    int teleportCount;

    [SerializeField]
    float teleportResetDuration;
    float currentTeleportResetDuration;

    [SerializeField]
    float slowMotionDuration = 3f;

    [SerializeField]
    TimeManager timeManager;

    [SerializeField]
    Slider[] teleportBars;

    [SerializeField]
    Slider slowMotionBar;
    bool canTeleport;
    float currentSlowMotionDuration;

    void Awake()
    {
        currentSlowMotionDuration = slowMotionDuration;
        currentTeleportResetDuration = teleportResetDuration;
        teleportCount = maxTeleportCount;
        canTeleport = true;

        foreach (Slider bar in teleportBars)
        {
            bar.value = 1;
        }
    }

    void Update()
    {
        ResetTeleports();
        teleportCount = Mathf.Clamp(teleportCount, 0, maxTeleportCount);
        canTeleport = teleportCount > 0 ? true : false;
        slowMotionBar.value = currentSlowMotionDuration / slowMotionDuration;
    }

    public void DecreaseTeleportCount()
    {
        if (teleportCount <= 0)
            return;

        teleportCount--;
        for (int i = teleportCount; i < teleportBars.Length; i++)
        {
            teleportBars[i].value = 0;
        }
    }

    public void GainTeleportCount()
    {
        if (teleportCount >= maxTeleportCount)
            return;
        teleportCount++;
        teleportBars[teleportCount - 1].value = 1;
    }

    void ResetTeleports()
    {
        if (teleportCount == maxTeleportCount)
            return;
        if (currentTeleportResetDuration <= Mathf.Epsilon)
        {
            teleportCount++;
            currentTeleportResetDuration = teleportResetDuration;
        }
        else
        {
            currentTeleportResetDuration -= Time.deltaTime;
            teleportBars[teleportCount].value =
                1 - currentTeleportResetDuration / teleportResetDuration;
        }
    }

    public void DecreaseSlowMotionDuration()
    {
        if (currentSlowMotionDuration >= Mathf.Epsilon)
        {
            currentSlowMotionDuration -= Time.unscaledDeltaTime;
        }
        else
        {
            timeManager.ResetTime();
        }
    }

    public void ResetSlowMotionDuration()
    {
        currentSlowMotionDuration = slowMotionDuration;
    }

    public bool GetCanTeleport()
    {
        return canTeleport;
    }
}
