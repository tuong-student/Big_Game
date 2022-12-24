using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   [SerializeField]
   private bool isSlow, canRotate;
//    [SerializeField]
//    private bool poolBullet;
   [SerializeField]
   private float deactivateTimer = 5f;
   [SerializeField]
    private float damage = 10f;
    private Rigidbody2D myBody;
    private Animator anim;
    private bool dealthDamage;
    private void Awake() {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start() {
        Invoke("DeactivateBullet",deactivateTimer);    
    }
    private void FixedUpdate() {
        if(isSlow)
            myBody.velocity= Vector2.Lerp(myBody.velocity,
                Vector2.zero,Random.value*Time.deltaTime);
        if(canRotate)
            transform.Rotate(Vector3.forward*60f);
    }
    void DeactivateBullet(){
        // if(poolBullet)
        //     gameObject.SetActive(false);
        // else 
        Destroy(gameObject);
    }
    // private void OnEnable() {
        
    // }
    // private void OnDisable() {
    //     transform.rotation=Quaternion.identity;
    //     isSlow = false;
    // }
    public void SetIsSlow(bool _isSlow){
        isSlow = _isSlow;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Blocking")||
            other.CompareTag("Player"))
        {
            if(other.CompareTag("Blocking"))
                myBody.velocity = Vector2.zero;

            CancelInvoke("DeactivateBullet");
            anim.SetTrigger("Explode");

            if(other.CompareTag("Player"))
            {
                if(!dealthDamage){
                    dealthDamage = true;
                    other.GetComponent<BaseCharacter>().Damage(damage);
                }
            }
        }
    }

}
