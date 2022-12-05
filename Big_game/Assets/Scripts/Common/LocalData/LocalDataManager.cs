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
    public static float fireRate;
    public static float criticalRate;
    public static float speed;

    public static void Load()
    {
        currentGun1Index = PlayerPrefs.GetInt(KeyManager.gun1Index);
        currentGun2Index = PlayerPrefs.GetInt(KeyManager.gun2Index);
        playerNumber = PlayerPrefs.GetInt(KeyManager.playerNumer);
        currentLevel = PlayerPrefs.GetInt(KeyManager.level);
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(KeyManager.playerNumer, playerNumber);
        PlayerPrefs.SetInt(KeyManager.gun1Index, currentGun1Index);
        PlayerPrefs.SetInt(KeyManager.gun1Index, currentGun1Index);
        PlayerPrefs.SetInt(KeyManager.level, currentLevel);

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
}
