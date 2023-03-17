using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using Game.Player;

namespace Game.Enemy
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField]
        private bool isSlow, canRotate;
    //    [SerializeField]
    //    private bool poolBullet;
        [SerializeField] private float deactivateTimer = 5f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private Animator anim;
        private Rigidbody2D myBody;
        private bool dealthDamage;

        private void Awake() {
            myBody = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            NOOD.NoodyCustomCode.StartDelayFunction(DestroyBullet, deactivateTimer);
        }

        private void FixedUpdate() {
            if(isSlow)
                myBody.velocity= Vector2.Lerp(myBody.velocity,
                    Vector2.zero,Random.value*Time.deltaTime);
            if (canRotate)
                transform.Rotate(Vector3.forward * 60f);

        }
        void DestroyBullet(){
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
                other.CompareTag("Player") || other.CompareTag("Door"))
            {
                if(other.CompareTag("Blocking") || other.CompareTag("Door"))
                    myBody.velocity = Vector2.zero;

                anim.SetTrigger("Explode");

                if(other.CompareTag("Player"))
                {
                    if(!dealthDamage){
                        dealthDamage = true;
                        other.GetComponent<PlayerScripts>().Damage(damage);
                    }
                }
                this.gameObject.SetActive(false);
            }
        }

    }
}
