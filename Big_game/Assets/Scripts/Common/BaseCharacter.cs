using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;
using NOOD.NoodCamera;
using Game.UI;

namespace Game.Base
{
    public class BaseCharacter : AbstractMonoBehaviour, IDamageable
    {
        #region Event
        public EventHandler <OnHealthChangeEventArgs> OnHealthChange;
        public class OnHealthChangeEventArgs : EventArgs
        {
            public float health;
            public float maxHealth;
        }
        public EventHandler <OnManaChangeEventArgs> OnManaChange;
        public class OnManaChangeEventArgs : EventArgs
        {
            public float mana;
            public float maxMana;
        }
        #endregion

        //[SerializeField] ParticleSystem bloodEff;
        public static BaseCharacter Instance { get; private set; }

        #region Stats
        public float maxHealth = 100f;
        public float currentHealth = 100f;
        public float maxMana = 50f;
        public float currentMana = 50f;
        public float playerDamage = 0f;
        public float criticalRate = 1f;
        public float fireRate = 0f;
        public float defence = 0f;
        public float stamina = 50f;
        public float walkSpeed = 0.5f;
        public float runSpeed = 0.8f;
        public float dashForce = 30f;
        internal float currentSpeed;

        public float dashTime = 0.5f;

        internal bool isDead = false;
        #endregion

        #region Bool

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

        public virtual void AddHealth(float amount)
        {
            this.currentHealth += amount;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
        }

        public virtual void MinusHealth(float amount)
        {
            this.currentHealth -= (amount - defence);
        }

        public virtual void AddMana(float amount)
        {
            this.currentMana += amount;
            if (currentMana > maxMana) currentMana = maxMana;
        }

        public virtual void MinusMana(float amount)
        {
            this.currentMana -= amount;
        }

        public virtual void Die()
        {
            //InGameUI.GetInstance.SetHealth(0);
            EventManager.GetInstance.OnLoseGame.RaiseEvent();
            Destroy(this.gameObject, 2f);
        }

    
    }
}
