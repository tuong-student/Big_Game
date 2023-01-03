using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUI : MonoBehaviour
{
    public void NewGame()
    {
        LocalDataManager.currentGun1Index = 0;
        LocalDataManager.currentGun2Index = 0;
        LocalDataManager.playerNumber = 0;
        LocalDataManager.gold = 0;
        LocalDataManager.currentLevel = 1;
        LocalDataManager.bonusDamage = 0f;
        LocalDataManager.bonusFireRate = 0;
        LocalDataManager.bonusReloadSpeed = 0;
        LocalDataManager.criticalRate = 0.5f;
        LocalDataManager.runSpeed = 0.8f;
        LocalDataManager.walkSpeed = 0.5f;
        LocalDataManager.defence = 0f;

        LocalDataManager.maxHealth = 100;
        LocalDataManager.mana = 50;
        LocalDataManager.Save();
        LocalDataManager.SaveGold();
        EventManager.GetInstance.OnNewGame.RaiseEvent();
    }
}
