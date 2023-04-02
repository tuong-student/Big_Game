using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        musicImage = musicButton.gameObject.GetComponent<Image>();
        soundImage = soundButton.gameObject.GetComponent<Image>();
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
        isMusicOn = !isMusicOn;
        if (isMusicOn)
        {
            musicImage.sprite = musicOnImage;

        }
        else
        {
            musicImage.sprite = musicOffImage;

        }
    }

    private void UpdateSoundButtonImage()
    {
        isSoundOn = !isSoundOn;
        if (isSoundOn)
        {
            soundImage.sprite = soundOnImage;

        }
        else
        {
            soundImage.sprite = soundOffImage;

        }
    }

    public void MusicVolume()
    {

    }

    public void SFXVolume()
    {

    }

    private void BackToMainMenu()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }

}
