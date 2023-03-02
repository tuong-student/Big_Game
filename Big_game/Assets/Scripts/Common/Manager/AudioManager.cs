using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NOOD;

public class AudioManager : MonoBehaviorInstance<AudioManager>
{
    public Sound[] musicSound, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public static AudioManager Create(Transform parent = null)
    {
        return Instantiate<AudioManager>(Resources.Load<AudioManager>("Prefabs/Manager/AudioManager"), parent);
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
        Sound s = Array.Find(sfxSounds, x => x.soundtype == soundName);
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
        //if(LocalDataManager.musicsetting == 0)
        //{
        //    musicSource.volume = 0;
        //    Debug.Log(LocalDataManager.musicsetting);

        //}
        //else
        //{
        //    musicSource.volume = 1;
        //    PlayMusic("Theme");
        //    musicSource.loop = true;
        //    Debug.Log(LocalDataManager.musicsetting);

        //}

    }

    public void ToggleSFX()
    {
        //if (LocalDataManager.soundsetting == 0)
        //{
        //    sfxSource.volume = 0;
        //    Debug.Log(LocalDataManager.soundsetting);

        //}
        //else
        //{
        //    sfxSource.volume = 1;
        //    Debug.Log(LocalDataManager.soundsetting);

        //}
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
