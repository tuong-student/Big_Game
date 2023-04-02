using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

namespace Game.Common.Manager
{
    public class PoolingManager : MonoBehaviour, Game.Common.Interface.ISingleton
    {
        [SerializeField] private ObjectPool bulletPooling, explodePooling, damageTextPooling;

        void Awake()
        {
            RegisterToContainer();
        }

        public static PoolingManager Create(Transform parent = null)
        {
            return Instantiate(Resources.Load<PoolingManager>("Prefabs/Manager/PoolingManager"), parent);
        }

        public void SetBulletPoolingObject(GameObject bulletPrefab)
        {
            bulletPooling.objectToPool = bulletPrefab;
        }

        public void SetExplodePoolingObject(GameObject explodePrefab)
        {
            explodePooling.objectToPool = explodePrefab;
        }

        public void SetDamageTextPoolingObject(GameObject damageText)
        {
            damageTextPooling.objectToPool = damageText;
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
            return damageTextPooling.GetPoolObject();
        }

        private void OnDestroy()
        {
            if(bulletPooling)
                Destroy(bulletPooling.gameObject);
            if(explodePooling)
                Destroy(explodePooling.gameObject);
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnregisterToContainer()
        {
            SingletonContainer.UnRegister(this);
        }
    }
}
