using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public float mana = 100f;
    public float stamina = 50f;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashForce = 30f;
    public float currentSpeed;

    public float dashTime = 0.5f;

    bool isDead = false;

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
