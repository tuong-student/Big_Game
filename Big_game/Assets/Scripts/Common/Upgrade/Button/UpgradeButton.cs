using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeButton : BaseButton
{
    [SerializeField] Text buttonText, goldNeedText;
    Upgrade upgrade;

    public static UpgradeButton Create(Transform parent)
    {
        return Instantiate<UpgradeButton>(Resources.Load<UpgradeButton>("Prefabs/Game/Upgrade/UpgradeBtn"), parent);
    }

    public void SetBtn(Upgrade upgrade = null, Action newAction = null)
    {
        action = newAction;
        if(upgrade != null)
        {
            this.upgrade = upgrade;
            this.buttonText.text = upgrade.upgradeStats.ToString() + " + " + upgrade.upgradeAmount;
            this.goldNeedText.text = upgrade.goldNeed.ToString("0.00");
        }
    }

    private void OnDestroy()
    {
        action = null;
    }
}
