using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonElement : BaseButton
{
    [SerializeField] Text buttonText, goldNeedText;
    Upgrade upgrade;
    Action action;

    public static ButtonElement Create(Transform parent)
    {
        return Instantiate<ButtonElement>(Resources.Load<ButtonElement>("Prefabs/Game/Upgrade/UpgradeBtn"), parent);
    }

    public void SetBtn(Upgrade upgrade = null, Action action = null)
    {
        this.action = action;
        if(upgrade != null)
        {
            this.upgrade = upgrade;
            this.buttonText.text = upgrade.upgradeStats.ToString();
            this.goldNeedText.text = upgrade.goldNeed.ToString("0.00");
        }
    }

    public void PerformUpgrade()
    {
        this.upgrade.AddStats();
    }

    private void OnDestroy()
    {
        action = null;
    }
}
