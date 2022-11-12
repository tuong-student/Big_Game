using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private CanvasGroup statsMenuCvg;
    [SerializeField] private RectTransform statsMenuRect;


    public int maxHealth = 50;
    public int currenHealth;

    public void Start()
    {
        MoveIn(statsMenuRect);
        currenHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currenHealth -= damage;
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        SetHealth(currenHealth);
    }

    public virtual void MoveIn(RectTransform rect)
    {
        Tween tweenContain = rect.DOAnchorPosX(0, 50).SetEase(Ease.InQuad).Play();
        tweenContain.Play();
    }

    public void MoveOut(RectTransform rect)
    {
        float anchorPosX = -1300;
        Tween tweenContain = rect.DOAnchorPosX(anchorPosX, 0.05f).SetEase(Ease.InQuad);
        tweenContain
            .Play();
    }
}
