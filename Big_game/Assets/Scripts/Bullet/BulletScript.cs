using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

namespace Game.Player
{
    public class BulletScript : MonoBehaviour
    {
        private readonly List<string> EFFECT_TAGS = new List<string> { "Enemy", "Blocking", "Door", "Boss" };

        [HideInInspector] public float damage = 1;
        [HideInInspector] public float backForce = 10f;
        public ExplodeType ExplodeType;
        private bool isBlock;

        private void OnEnable()
        {
            isBlock = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (EFFECT_TAGS.Contains(collision.gameObject.tag))
            {
                if(collision.TryGetComponent(out BaseEnemy enemy))
                {
                    DamageToEnemy(enemy);
                }

                this.gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            if (isBlock == true) return;

            // Create explode;
            GameObject explodePref = ExplodeManager.GetInstance.GetExplodePref(ExplodeType);
            PoolingManager.GetInstance.SetExpldePoolingObject(explodePref);
            GameObject explode = PoolingManager.GetInstance.GetExplode();
            explode.transform.position = this.transform.position;
            explode.GetComponent<ParticleSystem>().Play();
        }

        public void SetRange(float range)
        {
            GetComponent<SetObjectDeactive>().second = range;
        }

        public void SetBackForce(float backForce)
        {
            this.backForce = backForce;
        }

        private void DamageToEnemy(BaseEnemy enemy)
        {
            if (enemy != null && enemy.IsAlive())
            {
                //AudioManager.GetInstance.PlaySFX(sound.hitEnemy);
                enemy.TakeDamage(damage);
                enemy.GetComponent<Rigidbody2D>().AddForce(this.gameObject.transform.right * backForce);
            }
        }

    }
}
