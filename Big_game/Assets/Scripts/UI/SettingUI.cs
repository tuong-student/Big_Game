using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public Sprite musicOnImage;
    public Sprite musicOffImage;
    [SerializeField] private Button musicButton;
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    [SerializeField] public Button soundButton;
    public bool isMusicOn = true;
    public bool isSoundOn = true;
    [SerializeField] public Button backButton;


    private void Start()
    {
        if (isMusicOn) { musicButton.image.sprite = soundOnImage; }
        else { musicButton.image.sprite = soundOffImage; }
        if (isSoundOn) { soundButton.image.sprite = soundOnImage; }
        else { soundButton.image.sprite = soundOffImage; }
    }
    private void OnEnable()
    {
        musicButton.onClick.AddListener(UpdateMusicButtonImage);

        soundButton.onClick.AddListener(UpdateSoundButtonImage);

    }
    private void OnDisable()
    {
        musicButton.onClick.RemoveListener(UpdateMusicButtonImage);
        soundButton.onClick.RemoveListener(UpdateSoundButtonImage);

    }

    private void UpdateMusicButtonImage()
    {
        //AudioManager.instance.PlayManagerSound(Soundame.ButtonClick);
        //UserData.SoundSetting = !UserData.SoundSetting;
        musicButton.image.sprite = (isMusicOn) ? musicOnImage : musicOffImage;
    }

    private void UpdateSoundButtonImage()
    {
        soundButton.image.sprite = (isSoundOn) ? musicOnImage : musicOffImage;
    }
}
