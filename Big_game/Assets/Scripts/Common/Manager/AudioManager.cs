using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NOOD;
using Game.Save;

public class AudioManager : MonoBehaviour, Game.Common.Interface.ISingleton
{
    public Sound[] musicSound, sfxSounds;
    public AudioSource musicSource, sfxSource;
    private SaveModels.UserSetting userSetting;

    public static AudioManager Create(Transform parent = null)
    {
        return Instantiate<AudioManager>(Resources.Load<AudioManager>("Prefabs/Manager/AudioManager"), parent);
    }

    void Awake()
    {
        RegisterToContainer();
        userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        if(userSetting == null)
        {
            userSetting = new SaveModels.UserSetting();
            userSetting.musicVolume = 1;
            userSetting.soundVolume = 1;
            SaveJson.SaveToJson(userSetting, SaveModels.SaveFile.UserSettingSave.ToString());
        }
    }

    private void Start()
    {
        ToggleMusic();
        ToggleSFX();
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, x => x.name == name);
        if(s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }


    public void PlaySFX(sound soundName)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundType == soundName);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }


    public void ToggleMusic()
    {
        userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        musicSource.volume = userSetting.musicVolume;
    }

    public void ToggleSFX()
    {
        userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        sfxSource.volume = userSetting.soundVolume;
    }

    public void SetMusicVolume (float volume)
    {
        musicSource.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void RegisterToContainer()
    {
        SingletonContainer.Register(this);
    }

    public void UnregisterToContainer()
    {
        SingletonContainer.UnRegister(this);
    }

    public float GetMusicVolume()
    {
        userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        return userSetting.musicVolume;
    }

    public float GetSoundVolume()
    {
        userSetting = LoadJson<SaveModels.UserSetting>.LoadFromJson(SaveModels.SaveFile.UserSettingSave.ToString());
        return userSetting.soundVolume;
    }
}
