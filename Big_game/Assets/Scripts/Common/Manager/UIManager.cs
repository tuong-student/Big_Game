using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;

public class UIManager : MonoBehaviorInstance<UIManager>
{
    public static UIManager Create(Transform parent = null)
    {
        return Instantiate<UIManager>(Resources.Load<UIManager>("Prefabs/Manager/UIManager"), parent);
    }

    public void RefreshGoldText()
    {
        GameCanvas.GetInstance.SetGoldText("Gold: " + GoldManager.GetInstance.gold);
    }
}
