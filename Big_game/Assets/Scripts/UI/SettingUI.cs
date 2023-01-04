using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public Sprite musicOnImage;
    public Sprite musicOffImage;
    [SerializeField] private Button musicButton, soundButton, backButton;
    public Sprite soundOnImage, soundOffImage;
    [SerializeField] public Slider musicSlider, soundSlider;
    [SerializeField] public GameObject MainMenu;
    

    // public bool isMusicOn = true;
    // public bool isSoundOn = true;


    private void Start()
    {
//        LocalDataManager.Load();

        if (LocalDataManager.musicsetting == 1) { musicButton.image.sprite = musicOnImage; }
        else { musicButton.image.sprite = musicOffImage; }
        if (LocalDataManager.soundsetting == 1) { soundButton.image.sprite = soundOnImage; }
        else { soundButton.image.sprite = soundOffImage; }

        musicSlider.value = LocalDataManager.musicsetting;
        soundSlider.value = LocalDataManager.soundsetting;

    }
    private void OnEnable()
    {
        musicButton.onClick.AddListener(UpdateMusicButtonImage);
        soundButton.onClick.AddListener(UpdateSoundButtonImage);
        backButton.onClick.AddListener(BackToMainMenu);
    }

    private void OnDisable()
    {
        musicButton.onClick.RemoveListener(UpdateMusicButtonImage);
        soundButton.onClick.RemoveListener(UpdateSoundButtonImage);
        backButton.onClick.RemoveListener(BackToMainMenu);

    }

    private void UpdateMusicButtonImage()
    {
        //AudioManager.instance.PlayManagerSound(Soundame.ButtonClick);
        //UserData.SoundSetting = !UserData.SoundSetting;
        //musicButton.image.sprite = (LocalDataManager.soundsetting ==0) ? musicOnImage : musicOffImage;

        if (LocalDataManager.musicsetting == 0)
        {
            AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            musicButton.image.sprite = musicOnImage;
            LocalDataManager.musicsetting = 1;
            musicSlider.value = LocalDataManager.musicsetting;
            AudioManager.GetInstance.ToggleMusic();
            LocalDataManager.Save();
        }
        else
        {
            AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            musicButton.image.sprite = musicOffImage;
            LocalDataManager.musicsetting = 0;
            musicSlider.value = LocalDataManager.musicsetting;
            AudioManager.GetInstance.ToggleMusic();
            LocalDataManager.Save();
        }

    }

    private void UpdateSoundButtonImage()
    {


        //soundButton.image.sprite = (isSoundOn) ? musicOnImage : musicOffImage;

        if (LocalDataManager.soundsetting == 0)
        {
            soundButton.image.sprite = soundOnImage;
            LocalDataManager.soundsetting = 1;
            soundSlider.value = LocalDataManager.soundsetting;
            AudioManager.GetInstance.ToggleSFX();
            LocalDataManager.Save();
        }

        
        else
        {
            soundButton.image.sprite = soundOffImage;
            AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            LocalDataManager.soundsetting = 0;
            soundSlider.value = LocalDataManager.soundsetting;
            AudioManager.GetInstance.ToggleSFX();
            LocalDataManager.Save();
        }
    }

    public void MusicVolume()
    {
        AudioManager.GetInstance.MusicVolume(musicSlider.value);
        LocalDataManager.musicsetting = musicSlider.value;
    }

    public void SFXVolume()
    {
        AudioManager.GetInstance.SFXVolume(soundSlider.value);
        LocalDataManager.soundsetting = soundSlider.value;
    }

    private void BackToMainMenu()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
        AudioManager.GetInstance.PlaySFX(sound.buttonClick);
    }

}
