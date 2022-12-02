using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter :AbstractMonoBehaviour, IDamageable
{
    [SerializeField] ParticleSystem bloodEff;
    public static BaseCharacter Instance { get; private set; }

    #region Stats
    public float health = 100f;
    public float mana = 100f;
    public float stamina = 50f;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    //public float dashSpeed = 10f;
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
        health -= damageAmount;
        bloodEff?.Play();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject, 10f);
    }

    
}
