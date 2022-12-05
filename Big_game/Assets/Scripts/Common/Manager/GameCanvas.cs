using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviorInstance<GameCanvas>
{
    public Text goldText;

    public static GameCanvas Create(Transform parent = null)
    {
        return Instantiate<GameCanvas>(Resources.Load<GameCanvas>("Prefabs/Canvas/GameCanvas"), parent);
    }

    public UpgradePanel CreateUpgradePanel()
    {
        if (GameObject.FindObjectOfType<UpgradePanel>() != null) return null;
        return Instantiate<UpgradePanel>(Resources.Load<UpgradePanel>("Prefabs/Game/Upgrade/UpgradePanel"), this.transform);
    }

    public void SetGoldText(string text)
    {
        goldText.text = text;
    }
}
