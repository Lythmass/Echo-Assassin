using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject panel;

    bool isPaused = false;

    InputAction pauseAction;

    void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        panel.SetActive(true);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        panel.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
}
