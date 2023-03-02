using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enemy;

namespace Game.System.Enemy
{
    public enum EnemyBulletType
    {
        Normal,
        Srepad,
        SlowSpread
    }
    public class EnemyShooterController : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyBullet;
        [SerializeField]
        private float numberOfBullets = 3f;
        [SerializeField]
        private EnemyBulletType enemyBulletType = EnemyBulletType.Normal;
        [SerializeField]
        private float bulletSpeed = 1f;
        public void Shoot(Vector3 direction, Vector3 origin){
            if(enemyBulletType == EnemyBulletType.Normal){
                NormalShoot(direction,origin);
            }
            if(enemyBulletType == EnemyBulletType.Srepad){
                SpreadShoot(direction,origin,false);
            }
            if(enemyBulletType == EnemyBulletType.SlowSpread){
                SpreadShoot(direction,origin,true);
            }
        }
        public void ShootOnRandom(Vector3 direction, Vector3 origin){
            int randomShoot = Random.Range(0,2);
            if(randomShoot == 0)
                NormalShoot(direction,origin);

            if(randomShoot == 1)
                SpreadShoot(direction,origin,false);

            if(randomShoot == 2)
                SpreadShoot(direction,origin, true);
        }
        void NormalShoot(Vector3 direction, Vector3 origin){
            float offset = 0.5f;
            Quaternion rot = Quaternion.Euler(0,0,
                Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg);

            GameObject bullet = Instantiate(enemyBullet,origin,rot);
            bullet.GetComponent<Rigidbody2D>().velocity = direction*bulletSpeed;
            direction.x += Random.Range(-offset,offset);
            direction.y += Random.Range(-offset,offset);
        }
    
        void SpreadShoot(Vector3 direction, Vector3 origin, bool isSlow){
            float offset = 0.5f;
           for(int i = 0; i < numberOfBullets; i++)
           {
                Quaternion rot = Quaternion.Euler(0,0,
                    Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg);
                GameObject bullet = Instantiate(enemyBullet,origin,rot);
                bullet.GetComponent<Rigidbody2D>().velocity = direction*bulletSpeed/2f;
                bullet.GetComponent<EnemyBullet>().SetIsSlow(isSlow);
                direction.x += Random.Range(-offset,offset);
                direction.y += Random.Range(-offset,offset);
           } 
        }
    }
}
