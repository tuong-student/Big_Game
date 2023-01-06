using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeButton : BaseButton
{
    [SerializeField] Text buttonText, goldNeedText;

    public static UpgradeButton Create(Transform parent)
    {
        return Instantiate<UpgradeButton>(Resources.Load<UpgradeButton>("Prefabs/Game/Upgrade/UpgradeBtn"), parent);
    }

    public void SetBtn(Upgrade upgrade = null, Action newAction = null)
    {
        action = newAction;
        if(upgrade != null)
        {
            Debug.Log(upgrade.upgradeStats);
            this.buttonText.text = upgrade.upgradeStats.ToString() + "+" + upgrade.upgradeAmount;
            this.goldNeedText.text = upgrade.goldNeed.ToString("0");
        }
    }

    private void OnDestroy()
    {
        action = null;
    }
}
