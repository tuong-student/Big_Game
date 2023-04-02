using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using Game.Common.Manager;
using NOOD;

namespace Game.Player
{
    public class BulletScript : AbstractMonoBehaviour
    {
        private readonly List<string> EFFECT_TAGS = new List<string> { "Enemy", "Blocking", "Door", "Boss" };

        [HideInInspector] public float damage = 1;
        [HideInInspector] public float backForce = 10f;
        public ExplodeType ExplodeType;
        private bool isBlock;
        public bool isCritical = false;

        private PoolingManager poolingManager;

        private void OnEnable()
        {
            isBlock = false;
        }

        private void Start()
        {
            poolingManager = SingletonContainer.Resolve<PoolingManager>();
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

            // Create explode
            if (!poolingManager) return;
            GameObject explodePref = SingletonContainer.Resolve<ExplodeManager>().GetExplodePref(ExplodeType);
            poolingManager.SetExplodePoolingObject(explodePref);
            GameObject explode = poolingManager.GetExplode();
            explode.transform.position = this.transform.position;
            explode.GetComponent<ParticleSystem>().Play();
        }

        public void SetRange(float range)
        {
            GetComponent<SetObjectDeactivate>().second = range;
        }

        public void SetBackForce(float backForce)
        {
            this.backForce = backForce;
        }

        private void DamageToEnemy(BaseEnemy enemy)
        {
            if (enemy != null && enemy.IsAlive())
            {
                enemy.TakeDamage(damage);
                enemy.GetComponent<Rigidbody2D>().AddForce(this.gameObject.transform.right * backForce);

                // Create damageText
                if (!SingletonContainer.Resolve<DamageTextManager>()) return;
                GameObject damageText = SingletonContainer.Resolve<DamageTextManager>().CreateDamageText(damage, isCritical);
                damageText.transform.position = enemy.transform.position;
            }
        }
    }
}
