using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviorInstance<GoldManager>
{
    public static GoldManager i;
    public int gold;

    public static GoldManager Create(Transform parent = null)
    {
        return Instantiate<GoldManager>(Resources.Load<GoldManager>("Prefabs/Manager/GoldManager"), parent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AddGold(100);
        }
    }

    public bool MinusGold(int amount)
    {
        if(LocalDataManager.gold >= amount)
        {
            LocalDataManager.gold -= amount;
            gold -= amount;
            UIManager.i.RefreshGoldText();
            LocalDataManager.Save();
            return true;    
        }
        else
        {
            return false;
        }
    }

    public void AddGold(int amount)
    {
        LocalDataManager.gold += amount;
        gold += amount;
        UIManager.i.RefreshGoldText();
        LocalDataManager.Save();
    }
}
