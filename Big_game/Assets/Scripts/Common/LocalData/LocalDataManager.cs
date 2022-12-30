using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class LocalDataManager : MonoBehaviorInstance<LocalDataManager>
{
    public static int isSaveBefore = 0;

    public static int currentGun1Index;
    public static int currentGun2Index;
    public static int playerNumber = 0;
    public static int gold = 100;
    public static int currentLevel = 1;
    public static float bonusDamage = 1f;
    public static float bonusFireRate = 0;
    public static float bonusReloadSpeed = 0;
    public static float criticalRate = 0.5f;
    public static float runSpeed = 0.8f;
    public static float walkSpeed = 0.5f;
    public static float defence = 0f;

    public static float maxHealth = 100;
    public static float mana = 50;
    public static float musicsetting = 1;
    public static float soundsetting = 1;
   
    public static void LoadInit()
    { 
        isSaveBefore = PlayerPrefs.GetInt(KeyManager.isSaveBefore);
    }
    
    public static void Load()
    {

        currentGun1Index = PlayerPrefs.GetInt(KeyManager.gun1Index);
        currentGun2Index = PlayerPrefs.GetInt(KeyManager.gun2Index);
        playerNumber = PlayerPrefs.GetInt(KeyManager.playerNumer);
        currentLevel = PlayerPrefs.GetInt(KeyManager.level);
        gold = PlayerPrefs.GetInt(KeyManager.gold);

        maxHealth = PlayerPrefs.GetFloat(KeyManager.hp);
        mana = PlayerPrefs.GetFloat(KeyManager.mana);
        bonusDamage = PlayerPrefs.GetFloat(KeyManager.damage);
        criticalRate = PlayerPrefs.GetFloat(KeyManager.criticalRate);
        bonusFireRate = PlayerPrefs.GetFloat(KeyManager.fireRate);
        defence = PlayerPrefs.GetFloat(KeyManager.defence);
        runSpeed = PlayerPrefs.GetFloat(KeyManager.runSpeed);
        walkSpeed = PlayerPrefs.GetFloat(KeyManager.walkSpeed);
        bonusReloadSpeed = PlayerPrefs.GetFloat(KeyManager.reloadSpeed);

        musicsetting = PlayerPrefs.GetFloat(KeyManager.Music_Setting);
        soundsetting = PlayerPrefs.GetFloat(KeyManager.Sound_Setting);

        // musicsetting = PlayerPrefs
    }

    public static void Save()
    {

        PlayerPrefs.SetInt(KeyManager.isSaveBefore, 1);
        PlayerPrefs.SetInt(KeyManager.playerNumer, playerNumber);
        PlayerPrefs.SetInt(KeyManager.gun1Index, currentGun1Index);
        PlayerPrefs.SetInt(KeyManager.gun1Index, currentGun1Index);
        PlayerPrefs.SetInt(KeyManager.level, currentLevel);

        PlayerPrefs.SetFloat(KeyManager.damage, bonusDamage);
        PlayerPrefs.SetFloat(KeyManager.criticalRate, criticalRate);
        PlayerPrefs.SetFloat(KeyManager.fireRate, bonusFireRate);
        PlayerPrefs.SetFloat(KeyManager.runSpeed, runSpeed);
        PlayerPrefs.SetFloat(KeyManager.walkSpeed, walkSpeed);
        PlayerPrefs.SetFloat(KeyManager.reloadSpeed, bonusReloadSpeed);

        PlayerPrefs.SetFloat(KeyManager.hp, maxHealth);
        PlayerPrefs.SetFloat(KeyManager.mana, mana);
        PlayerPrefs.SetFloat(KeyManager.defence, defence);
        
        PlayerPrefs.Save();
        Load();
    }

    public static void SaveGold()
    {
        PlayerPrefs.SetInt(KeyManager.gold, gold);
        PlayerPrefs.Save();
    }

    public static void SaveSetting()
    {
        PlayerPrefs.SetFloat(KeyManager.Music_Setting, musicsetting);
        PlayerPrefs.SetFloat(KeyManager.Sound_Setting, soundsetting);
        PlayerPrefs.Save();
    }
}


public class KeyManager
{
    public static readonly string isSaveBefore = "isSaveBefore";

    public static readonly string playerNumer = "PlayerNumber";
    public static readonly string gold = "gold";
    public static readonly string level = "level";

    #region Stats
    public static readonly string fireRate = "fireRate";
    public static readonly string criticalRate = "criticalRate";
    public static readonly string reloadSpeed = "reloadSpeed";
    public static readonly string runSpeed = "runSpeed";
    public static readonly string walkSpeed = "walkSpeed";
    public static readonly string damage = "damage";
    public static readonly string defence = "defence";
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
