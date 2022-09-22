using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonElement : MonoBehaviour
{
    [SerializeField] Text buttonText, goldNeedText;
    [SerializeField] Button button;
    [SerializeField] Image background;
    Upgrade upgrade;
    Action action;

    public static ButtonElement Create(Transform parent)
    {
        return Instantiate<ButtonElement>(Resources.Load<ButtonElement>("Prefabs/Game/Upgrade/UpgradeBtn"), parent);
    }

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            action?.Invoke();
        });
        PopupAnimation();
    }

    void PopupAnimation(float duration = 0.5f)
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
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
