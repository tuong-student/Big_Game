using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NOOD;

public class AudioManager : MonoBehaviour, Game.Common.Interface.ISingleton
{
    public Sound[] musicSound, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public static AudioManager Create(Transform parent = null)
    {
        return Instantiate<AudioManager>(Resources.Load<AudioManager>("Prefabs/Manager/AudioManager"), parent);
    }

    void Awake()
    {
        RegisterToContainer();
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


    }

    public void ToggleSFX()
    {

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
}
