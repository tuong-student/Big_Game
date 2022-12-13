using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSound, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
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
        if(LocalDataManager.musicsetting == 0)
        {
            musicSource.volume = 0;
            Debug.Log(LocalDataManager.musicsetting);

        }
        else
        {
            musicSource.volume = 1;
            PlayMusic("Theme");
            musicSource.loop = true;
            Debug.Log(LocalDataManager.musicsetting);

        }

    }

    public void ToggleSFX()
    {
        if (LocalDataManager.soundsetting == 0)
        {
            sfxSource.volume = 0;
            Debug.Log(LocalDataManager.soundsetting);

        }
        else
        {
            sfxSource.volume = 1;
            Debug.Log(LocalDataManager.soundsetting);

        }
    }

    public void MusicVolume (float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }


}
