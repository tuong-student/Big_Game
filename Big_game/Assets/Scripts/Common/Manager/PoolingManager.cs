using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class PoolingManager : MonoBehaviorInstance<PoolingManager>
{
    public static PoolingManager i;
    [SerializeField] private ObjectPool bulletPooling, explodePooling;

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

    public GameObject GetBullet()
    {
        return bulletPooling.GetPoolObject();
    }

    public GameObject GetExplode()
    {
        return explodePooling.GetPoolObject();
    }

    private void OnDestroy()
    {
        if(bulletPooling)
            Destroy(bulletPooling.gameObject);
        if(explodePooling)
            Destroy(explodePooling.gameObject);
    }
}
