using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour, IDamageable
{
    //TODO: mang may cai nay sang base player, enemy khong can, mang toi '!'
    public float mana = 100f;
    public float stamina = 50f;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashForce = 30f;
    public float currentSpeed;
    public float dashTime = 0.5f;
    //! mang den day thoi
    [SerializeField]
    private float maxHealth = 100f ;
    [SerializeField]
    private float health;
    [SerializeField] 
    private Animator anim;
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    private void Start() {
        health = maxHealth;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if(health <= 0f)
        {
            anim.SetTrigger("Death");
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
    public bool IsAlive()
    {
        return health > 0 ? true:false; 
    }
}
