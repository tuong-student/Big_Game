using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NOOD;

public class InGameUI : MonoBehaviorInstance<InGameUI>
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private CanvasGroup statsMenuCvg;
    [SerializeField] private RectTransform statsMenuRect;
    [SerializeField] private Text fireRateText, criticalRateText, speedText, damageText, goldText;
    [SerializeField] private Image gun1, gun2;
    
    private bool isOn = false;

    public float maxHealth = 100;

    public float maxMana = 50;
    public float currentMana;

    public void Start()
    {
        SetMaxHealth(LocalDataManager.maxHealth);
        SetMaxMana(LocalDataManager.maxMana);
        ResetGoldText();

        EventManager.GetInstance.OnContinuewGame.OnEventRaise += ResetGoldText;
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    TakeDamage(20);
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    TakeDamage(-20);
        //}
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isOn)
            {
                MoveOut(statsMenuRect);
                isOn = false;
            }
            else
            {
                SetStats();
                MoveIn(statsMenuRect);
                isOn = true;
            }
        }
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    TakeMana(20);
        //}

    }

    public void TakeDamage(float damage)
    {
        if (LocalDataManager.maxHealth <= 0)
        {
            SetHealth(0);
            return;
        }

        SetHealth(LocalDataManager.maxHealth);
    }

    public void SetMaxHealth(float health)
    {
        //LocalDataManager.health = maxHealth;
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    public void ResetGoldText()
    {
        goldText.text = "Gold: " + LocalDataManager.gold.ToString("0");
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

    public void SetStats()
    {
        fireRateText.text = 
	    $"Fire Rate: {PlayerScripts.GetInstance.GetCurrentGunData().fireRate}+<color=#FF5353>{LocalDataManager.bonusFireRate}</color>";
        criticalRateText.text = "Crit Rate: " + LocalDataManager.criticalRate;
        speedText.text = "Speed: " + LocalDataManager.runSpeed;
        damageText.text = 
	    $"Damage: {PlayerScripts.GetInstance.GetCurrentGunData().damage}+<color=#FF5353>{LocalDataManager.bonusDamage}</color>";
    }


    public virtual void MoveIn(RectTransform rect)
    {
        Tween tweenContain = rect.DOLocalMoveX(-700, 0.3f).SetEase(Ease.OutExpo).Play();
        tweenContain.Play();
    }

    public void MoveOut(RectTransform rect)
    {
        float anchorPosX = -1300;
        Tween tweenContain = rect.DOLocalMoveX(anchorPosX, 0.05f).SetEase(Ease.InQuad);
        tweenContain.Play();
    }

    public void ChangeGunSprites(Sprite mainGun, Sprite subGun)
    {
        gun1.sprite = mainGun;
        gun2.sprite = subGun;
    }
}
