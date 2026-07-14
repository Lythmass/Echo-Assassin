using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject settingsMenu;

    [SerializeField]
    Slider volumeSlider;

    bool isChangingMenus = false;
    float musicVolume;

    void Start()
    {
        musicVolume = AudioManager.instance.GetMusicVolume();
    }

    public void Play()
    {
        AudioManager.instance.PlayUIClickSFX();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Options()
    {
        AudioManager.instance.PlayUIClickSFX();
        isChangingMenus = true;
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Back()
    {
        AudioManager.instance.PlayUIClickSFX();
        isChangingMenus = true;
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void Quit()
    {
        AudioManager.instance.PlayUIClickSFX();
        Application.Quit();
    }

    public void VolumeSlider()
    {
        AudioManager.instance.GetComponent<AudioSource>().volume = volumeSlider.value;
        AudioManager.instance.SetMusicVolume(volumeSlider.value * musicVolume);
    }

    public bool GetIsChangingMenus()
    {
        return isChangingMenus;
    }

    public void SetIsChangingMenusFalse()
    {
        isChangingMenus = false;
    }
}
