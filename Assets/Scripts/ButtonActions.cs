using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject settingsMenu;

    [SerializeField]
    Slider volumeSlider;

    [SerializeField]
    Toggle postProcessingToggle;

    bool isChangingMenus = false;
    float musicVolume;
    Volume globalVolume;

    void Start()
    {
        musicVolume = AudioManager.instance.GetMusicVolume();
        globalVolume = FindAnyObjectByType<Volume>();
        volumeSlider.value = GameController.instance.GetAudioVolume();
        postProcessingToggle.isOn = GameController.instance.GetPostProcessingEnabled();
    }

    public void Play()
    {
        AudioManager.instance.PlayUIClickSFX();
        LevelManager.instance.LoadNextLevel();
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
        GameController.instance.SetAudioVolume(volumeSlider.value);
        AudioManager.instance.GetComponent<AudioSource>().volume = volumeSlider.value;
        AudioManager.instance.SetMusicVolume(volumeSlider.value * musicVolume);
    }

    public void PostProcessingCheckbox()
    {
        GameController.instance.SetPostProcessingEnabled(postProcessingToggle.isOn);
        globalVolume.gameObject.SetActive(postProcessingToggle.isOn);
    }

    public bool GetIsChangingMenus()
    {
        return isChangingMenus;
    }

    public void SetIsChangingMenusFalse()
    {
        isChangingMenus = false;
    }

    public void LoadMainMenu()
    {
        AudioManager.instance.PlayUIClickSFX();
        LevelManager.instance.LoadMainMenu();
    }
}
