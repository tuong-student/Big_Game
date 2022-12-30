using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUI : MonoBehaviour
{
    /*
         public static float fireRate = 5;
    public static float criticalRate = 5;
    public static float speed = 5;
    public static float damage = 1f;
    public static float defence = 0f;

    public static float health = 100;
    public static float mana = 50;*/

    public void NewGame()
    {
        LocalDataManager.bonusFireRate = 5;
        LocalDataManager.criticalRate = 5;
        LocalDataManager.runSpeed = 5;
        LocalDataManager.bonusDamage = 1f;
        LocalDataManager.defence = 0f;
        LocalDataManager.maxHealth = 100;
        LocalDataManager.mana = 50;
        LocalDataManager.currentLevel = 1;
        LocalDataManager.gold = 0;

        //Main.GetInstance.GenerateNewLevel(); 
    }
}
