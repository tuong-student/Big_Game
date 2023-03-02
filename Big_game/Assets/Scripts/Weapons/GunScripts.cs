using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class GunScripts : MonoBehaviour
    {
        [HideInInspector] public GunData gunData;
        public SpriteRenderer sr;
        public Animator anim;
        [SerializeField] Transform shootPoint;

        #region
        ObjectPool bulletPooling;
        #endregion

        private void Awake()
        {
            if(sr == null)
                sr = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            //EventManager.GetInstance.OnCheatEnable.OnEventRaise += () => { isCheat = true; };
            //EventManager.GetInstance.OnCheatDisable.OnEventRaise += () => { isCheat = false; };
        }

        public void SetData(GunData data)
        {
            this.gunData = data;
            sr.sprite = data.gunImage;
        }

        public void Fire()
        {
            if (gunData == WeaponManager.GetInstance.shotgunData)
            {
                // Set pooling object is bullet
                PoolingManager.GetInstance.SetBulletPoolingObject(this.gunData.bulletPrefab);
                // Create 3 bullets
                GameObject[] bullets = new GameObject[3];
                bullets[0] = PoolingManager.GetInstance.GetBullet();
                bullets[1] = PoolingManager.GetInstance.GetBullet();
                bullets[2] = PoolingManager.GetInstance.GetBullet();
                foreach(var b in bullets)
                {
                    b.transform.position = shootPoint.transform.position;
                    SetBulletDamage(b, gunData.damage);
                }

                // Shoot 3 bullets to 3 different directions
                bullets[0].GetComponent<Rigidbody2D>().AddForce((new Vector3(-0.5f, -0.5f) + shootPoint.right).normalized * gunData.bulletForce * 10f);
                bullets[1].GetComponent<Rigidbody2D>().AddForce(shootPoint.right * gunData.bulletForce * 10f);
                bullets[2].GetComponent<Rigidbody2D>().AddForce((new Vector3(0.5f, 0.5f) + shootPoint.right).normalized * gunData.bulletForce * 10f);
            
            }
            else
            {
                PoolingManager.GetInstance.SetBulletPoolingObject(this.gunData.bulletPrefab);
                GameObject bullet = PoolingManager.GetInstance.GetBullet();
                BulletScript bulletScript = bullet.GetComponent<BulletScript>();
                bulletScript.SetRange(gunData.range);
                bullet.transform.SetPositionAndRotation(shootPoint.transform.position, shootPoint.transform.rotation);

                bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * gunData.bulletForce * 10f);
                SetBulletDamage(bullet, gunData.damage);
            }

            PlayGunSound();
            anim.SetTrigger("Shooting");
            anim.SetInteger("AnimIndex", gunData.animationIndex);
        }

        void PlayGunSound()
        { 
            switch(gunData.name)
            {
                //case "laser":
                //case "spazer":
                //case "flameThrower":
                //    AudioManager.GetInstance.PlaySFX(sound.laserWeapon);
                //    break;
                //case "matter":
                //    AudioManager.GetInstance.PlaySFX(sound.matterWeapon);
                //    break;
                //case "piston":
                //case "mg":
                //case "shotgun":
                //case "cannon":
                //case "rocket":
                //    AudioManager.GetInstance.PlaySFX(sound.pistolWeapon);
                //    break;
	        }
        }

        void SetBulletDamage(GameObject bullet, float damage)
        {
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            // In the future damage will be randomed base on critical rate
            
            bulletScript.damage = damage + PlayerScripts.GetInstance.playerDamage;
        }
    }
}
