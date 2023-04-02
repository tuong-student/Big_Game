using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common.Manager;

namespace Game.Player.Weapon
{
    public class GunScripts : MonoBehaviour
    {
        #region Singleton
        private WeaponManager weaponManager;
        private PoolingManager poolingManager;
        #endregion

        [HideInInspector] public GunData gunData;
        public SpriteRenderer sr;
        public Animator anim;
        [SerializeField] Transform shootPoint;


        ObjectPool bulletPooling;

        private void Awake()
        {
        }

        private void Start()
        {
            weaponManager = SingletonContainer.Resolve<WeaponManager>();
            poolingManager = SingletonContainer.Resolve<PoolingManager>();

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
            float bulletDamage = gunData.damage + SingletonContainer.Resolve<PlayerScripts>().damage.value;

            bool isCritical = UnityEngine.Random.Range(0f, 1f) * 100 <= SingletonContainer.Resolve<PlayerScripts>().criticalRate.value;
            if (isCritical) bulletDamage *= 2;

            if (gunData == weaponManager.shotgunData)
            {
                // Set pooling object is bullet
                poolingManager.SetBulletPoolingObject(this.gunData.bulletPrefab);
                // Create 3 bullets
                GameObject[] bullets = new GameObject[3];
                bullets[0] = poolingManager.GetBullet();
                bullets[1] = poolingManager.GetBullet();
                bullets[2] = poolingManager.GetBullet();
                foreach(var b in bullets)
                {
                    b.transform.position = shootPoint.transform.position;
                    SetBulletDamage(b, bulletDamage);
                    SetBulletIsCritical(b, isCritical);
                }

                // Shoot 3 bullets to 3 different directions
                bullets[0].GetComponent<Rigidbody2D>().AddForce((new Vector3(-0.5f, -0.5f) + shootPoint.right).normalized * gunData.bulletForce * 10f);
                bullets[1].GetComponent<Rigidbody2D>().AddForce(shootPoint.right * gunData.bulletForce * 10f);
                bullets[2].GetComponent<Rigidbody2D>().AddForce((new Vector3(0.5f, 0.5f) + shootPoint.right).normalized * gunData.bulletForce * 10f);
            
            }
            else
            {
                poolingManager.SetBulletPoolingObject(this.gunData.bulletPrefab);
                GameObject bullet = poolingManager.GetBullet();
                BulletScript bulletScript = bullet.GetComponent<BulletScript>();
                bulletScript.SetRange(gunData.range);
                bullet.transform.SetPositionAndRotation(shootPoint.transform.position, shootPoint.transform.rotation);

                bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * gunData.bulletForce * 10f);
                SetBulletDamage(bullet, bulletDamage);
                SetBulletIsCritical(bullet, isCritical);
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
            bulletScript.damage = damage;
        }

        private void SetBulletIsCritical(GameObject bullet, bool isCritical)
        {
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.isCritical = isCritical;
        }
    }
}
