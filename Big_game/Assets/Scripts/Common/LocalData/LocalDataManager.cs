using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDataManager : MonoBehaviorInstance<LocalDataManager>
{
    public static int currentGun1Index;
    public static int currentGun2Index;
    public static int playerNumber;
    public static int gold = 200;
    public static int currentLevel = 1;
    public static float fireRate = 5;
    public static float criticalRate = 5;
    public static float speed = 5;




    public static float health = 100;
    public static float mana = 50;
    public static float musicsetting = 1;
    public static float soundsetting = 1;


    public static void Load()
    {
        currentGun1Index = PlayerPrefs.GetInt(KeyManager.gun1Index);
        currentGun2Index = PlayerPrefs.GetInt(KeyManager.gun2Index);
        playerNumber = PlayerPrefs.GetInt(KeyManager.playerNumer);
        currentLevel = PlayerPrefs.GetInt(KeyManager.level);


        health = PlayerPrefs.GetFloat(KeyManager.hp);
        mana = PlayerPrefs.GetFloat(KeyManager.mana);
        musicsetting = PlayerPrefs.GetFloat(KeyManager.Music_Setting);
        soundsetting = PlayerPrefs.GetFloat(KeyManager.Sound_Setting);

        // musicsetting = PlayerPrefs.
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(KeyManager.playerNumer, playerNumber);
        PlayerPrefs.SetInt(KeyManager.gun1Index, currentGun1Index);
        PlayerPrefs.SetInt(KeyManager.gun1Index, currentGun1Index);
        PlayerPrefs.SetInt(KeyManager.level, currentLevel);


        PlayerPrefs.SetFloat(KeyManager.hp, health);
        PlayerPrefs.SetFloat(KeyManager.mana, mana);
        PlayerPrefs.SetFloat(KeyManager.Music_Setting, musicsetting);
        PlayerPrefs.SetFloat(KeyManager.Sound_Setting, soundsetting);
        PlayerPrefs.Save();
        Load();
    }
}


public class KeyManager
{
    public static readonly string playerNumer = "PlayerNumber";
    public static readonly string gold = "gold";
    public static readonly string level = "level";

    #region Stats
    public static readonly string fireRate = "fireRate";
    public static readonly string criticalRate = "criticalRate";
    public static readonly string speed = "speed";
    #endregion

    #region Guns
    public static readonly string gun1Index = "gun1";
    public static readonly string gun2Index = "gun2";
    #endregion



    public static readonly string Music_Setting = "player_music_setting";

    public static readonly string Sound_Setting = "player_sound_setting";

    public static readonly string hp = "player_health";
    public static readonly string mana = "player_mana";



}
