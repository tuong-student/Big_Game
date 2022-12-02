using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private CanvasGroup statsMenuCvg;
    [SerializeField] private RectTransform statsMenuRect;
    private bool isOn = false;


    public float maxHealth = 50;
    public float currenHealth;

    public float maxMana = 50;
    public float currentMana;

    public void Start()
    {
        SetMaxHealth(maxHealth);
        SetMaxMana(maxMana);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isOn)
            {
                MoveOut(statsMenuRect);
                isOn = false;
            }
            else
            {
                MoveIn(statsMenuRect);
                isOn = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeMana(20);
        }



    }

    public void TakeDamage(int damage)
    {
        currenHealth -= damage;
        SetHealth(currenHealth);
    }

    public void SetMaxHealth(float health)
    {
        currenHealth = maxHealth;
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    public void TakeMana(int mana)
    {
        currentMana -= mana;
        SetMana(currentMana);
    }

    public void SetMaxMana(float mana)
    {
        currentMana = maxMana;
        manaSlider.maxValue = mana;
        manaSlider.value = mana;
    }

    public void SetMana(float mana)
    {
        manaSlider.value = mana;
    }



    public virtual void MoveIn(RectTransform rect)
    {
        Tween tweenContain = rect.DOAnchorPosX(-600, 0.3f).SetEase(Ease.InQuad).Play();
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
