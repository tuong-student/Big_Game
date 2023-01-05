using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;
using NOOD.NoodCamera;

public class BaseCharacter : AbstractMonoBehaviour, IDamageable
{
    //[SerializeField] ParticleSystem bloodEff;
    public static BaseCharacter Instance { get; private set; }

    #region Stats
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float maxMana = 50f;
    public float currentMana = 50f;
    public float bonusDamage = 1f;
    public float criticalRate = 0f;
    public float fireRate = 0f;
    public float reloadSpeed = 0f;
    public float defence = 0f;
    public float stamina = 50f;
    public float walkSpeed = 0.5f;
    public float runSpeed = 0.8f;
    public float dashForce = 30f;
    internal float currentSpeed;

    public float dashTime = 0.5f;

    internal bool isDead = false;
    #endregion

    public virtual void OnEnable()
    {
        if (Instance == null) Instance = this;
        else
        {
            DestroyImmediate(Instance.gameObject);
            Instance = this;
        }
    }

    public void Damage(float damageAmount)
    {
        MinusHealth(damageAmount);
        CameraShake.GetInstance.Shake();
        //if (bloodEff) bloodEff.Play();
    }

    public void AddHealth(float amount)
    {
        this.currentHealth += amount;
        InGameUI.GetInstance.SetHealth(currentHealth);
    }

    public void MinusHealth(float amount)
    {
        this.currentHealth -= amount;
        InGameUI.GetInstance.SetHealth(currentHealth);
    }

    public void AddMana(float amount)
    {
        this.currentMana += amount;
        InGameUI.GetInstance.SetMana(currentMana);
    }

    public void MinusMana(float amount)
    {
        this.currentMana -= amount;
        InGameUI.GetInstance.SetMana(currentMana);
    }

    public virtual void Die()
    {
        InGameUI.GetInstance.SetHealth(0);
        EventManager.GetInstance.OnLoseGame.RaiseEvent();
        Destroy(this.gameObject, 1f);
    }

    
}
