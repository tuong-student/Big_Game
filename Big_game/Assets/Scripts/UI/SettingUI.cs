using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Save;

public class SettingUI : MonoBehaviour
{
    public Sprite musicOnImage;
    public Sprite musicOffImage;
    public Sprite soundOnImage, soundOffImage;
    private Image musicImage, soundImage;
    [SerializeField] private Button musicButton, soundButton, backButton;
    [SerializeField] public Slider musicSlider, soundSlider;
    [SerializeField] public GameObject MainMenu;

    private bool isMusicOn = true;
    private bool isSoundOn = true;

    private SaveModels.UserSetting userSetting;

    private void Start()
    {
        musicImage = musicButton.gameObject.GetComponent<Image>();
        soundImage = soundButton.gameObject.GetComponent<Image>();
        userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        UpdateAllSlider();
    }
    private void OnEnable()
    {
        musicButton.onClick.AddListener(UpdateMusicButtonImage);
        soundButton.onClick.AddListener(UpdateSoundButtonImage);
        backButton.onClick.AddListener(BackToMainMenu);
        musicSlider.onValueChanged.AddListener(MusicVolume);
        soundSlider.onValueChanged.AddListener(SFXVolume);
    }

    private void OnDisable()
    {
        musicButton.onClick.RemoveListener(UpdateMusicButtonImage);
        soundButton.onClick.RemoveListener(UpdateSoundButtonImage);
        backButton.onClick.RemoveListener(BackToMainMenu);
        musicSlider.onValueChanged.RemoveAllListeners();
        soundSlider.onValueChanged.RemoveAllListeners();
    }

    float oldMusicVolume = 0;
    private void UpdateMusicButtonImage()
    {
        if(userSetting == null)
        {
            userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        }

        isMusicOn = !isMusicOn;
        if (isMusicOn)
        {
            musicImage.sprite = musicOnImage;
            SingletonContainer.Resolve<AudioManager>().SetMusicVolume(oldMusicVolume);
        }
        else
        {
            musicImage.sprite = musicOffImage;
            oldMusicVolume = userSetting.musicVolume;
            SingletonContainer.Resolve<AudioManager>().SetMusicVolume(0);
        }
        userSetting.musicVolume = SingletonContainer.Resolve<AudioManager>().GetMusicVolume();
    }

    float oldSFXVolume = 0f;
    private void UpdateSoundButtonImage()
    {
        if(userSetting == null)
        {
            userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        }       
        
        isSoundOn = !isSoundOn;
        if (isSoundOn)
        {
            soundImage.sprite = soundOnImage;
            SingletonContainer.Resolve<AudioManager>().SetSFXVolume(oldSFXVolume);
        }
        else
        {
            soundImage.sprite = soundOffImage;
            oldSFXVolume = userSetting.soundVolume;
            SingletonContainer.Resolve<AudioManager>().SetSFXVolume(0);
        }
        userSetting.soundVolume = SingletonContainer.Resolve<AudioManager>().GetSoundVolume();
    }

    private void UpdateImage()
    {
        if(userSetting == null)
        {
            userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        }
        
        if(userSetting.soundVolume != 0)
        {
            isSoundOn = true;
            soundImage.sprite = soundOnImage;
        }
        else
        {
            isSoundOn = false;
            soundImage.sprite = soundOffImage;
        }

        if(userSetting.musicVolume != 0)
        {
            isMusicOn = true;
            musicImage.sprite = musicOnImage;
        }
        else
        {
            isMusicOn = false;
            musicImage.sprite = musicOffImage;
        }
    }

    private void UpdateAllSlider()
    {
        if(userSetting == null)
        {
            userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        }

        musicSlider.maxValue = 1;
        soundSlider.maxValue = 1;
        musicSlider.value = userSetting.musicVolume;
        soundSlider.value = userSetting.soundVolume;
    }

    public void MusicVolume(float volume)
    {
        SingletonContainer.Resolve<AudioManager>().SetMusicVolume(volume);
        userSetting.musicVolume = volume;
    }

    public void SFXVolume(float volume)
    {
        SingletonContainer.Resolve<AudioManager>().SetSFXVolume(volume);
        userSetting.soundVolume = volume;
    }

    private void BackToMainMenu()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
        SaveSetting();
    }

    private void SaveSetting()
    {
        SaveJson.SaveToJson(userSetting, SaveModels.SaveFile.UserSettingSave.ToString());
    }

}
