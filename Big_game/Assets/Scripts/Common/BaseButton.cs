using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BaseButton : MonoBehaviour
{
    [SerializeField] Button button;
    protected Action action;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            Debug.Log("Button Press");
            action?.Invoke();
        });
        PopupAnimation();
    }

    void PopupAnimation()
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
    }
}
