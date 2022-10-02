using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ExitPopUp : BaseButton
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;


    private void Start()
    {
        PopupAnimation();
    }

    void PopupAnimation()
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
    }

    private void TurnoffPanel()
    {

    }

}
