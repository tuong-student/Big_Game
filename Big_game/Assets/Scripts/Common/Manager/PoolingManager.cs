using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

namespace Game.Common.Manager
{
    public class PoolingManager : MonoBehaviorInstance<PoolingManager>
    {
        public static PoolingManager i;
        [SerializeField] private ObjectPool bulletPooling, explodePooling, damgageTextPooling;

        public static PoolingManager Create(Transform parent = null)
        {
            return Instantiate(Resources.Load<PoolingManager>("Prefabs/Manager/PoolingManager"), parent);
        }

        public void SetBulletPoolingObject(GameObject bulletPrefab)
        {
            bulletPooling.objectToPool = bulletPrefab;
        }

        public void SetExpldePoolingObject(GameObject explodePrefab)
        {
            explodePooling.objectToPool = explodePrefab;
        }

        public void SetDamageTextPoolingObject(GameObject damageText)
        {
            damgageTextPooling.objectToPool = damageText;
        }

        public GameObject GetBullet()
        {
            return bulletPooling.GetPoolObject();
        }

        public GameObject GetExplode()
        {
            return explodePooling.GetPoolObject();
        }

        public GameObject GetDamageText()
        {
            return damgageTextPooling.GetPoolObject();
        }

        private void OnDestroy()
        {
            if(bulletPooling)
                Destroy(bulletPooling.gameObject);
            if(explodePooling)
                Destroy(explodePooling.gameObject);
        }
    }
}
