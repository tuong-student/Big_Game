using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUI : MonoBehaviour
{
    /*
    public static int playerNumber = 1;
    public static int gold = 100;
    public static int currentLevel = 1;
    public static float bonusDamage = 1f;
    public static float bonusFireRate = 0;
    public static float bonusReloadSpeed = 0;
    public static float criticalRate = 0.5f;
    public static float runSpeed = 5f;
    public static float walkSpeed = 2f;
    public static float defence = 0f;

    public static float maxHealth = 100;
    public static float mana = 50;
    
     */

    public void NewGame()
    {
        LocalDataManager.playerNumber = 1;
        LocalDataManager.gold = 100;
        LocalDataManager.currentLevel = 1;
        LocalDataManager.bonusDamage = 1f;
        LocalDataManager.bonusFireRate = 0;
        LocalDataManager.bonusReloadSpeed = 0;
        LocalDataManager.criticalRate = 0.5f;
        LocalDataManager.runSpeed = 5f;
        LocalDataManager.walkSpeed = 2f;
        LocalDataManager.defence = 0f;
        LocalDataManager.maxHealth = 100;
        LocalDataManager.mana = 50;
    }

    public void TryAgain()
    {
        LocalDataManager.gold = 100;
        LocalDataManager.currentLevel = 1;
        LocalDataManager.bonusDamage = 1f;
        LocalDataManager.bonusFireRate = 0;
        LocalDataManager.bonusReloadSpeed = 0;
        LocalDataManager.criticalRate = 0.5f;
        LocalDataManager.runSpeed = 5f;
        LocalDataManager.walkSpeed = 2f;
        LocalDataManager.defence = 0f;
        LocalDataManager.maxHealth = 100;
        LocalDataManager.mana = 50;
    }
}
