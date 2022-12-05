using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviorInstance<UIManager>
{

    public static UIManager i;

    private void Awake()
    {
        if (i == null) i = this;
    }

    public static UIManager Create(Transform parent = null)
    {
        return Instantiate<UIManager>(Resources.Load<UIManager>("Prefabs/Manager/UIManager"), parent);
    }

    public void RefreshGoldText()
    {
        GameCanvas.i.SetGoldText("Gold: " + GoldManager.i.gold);
    }
}
