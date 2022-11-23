using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager i;
    [SerializeField] private ObjectPool bulletPooling, explodePooling;

    public static PoolingManager Create(Transform parent = null)
    {
        return Instantiate(Resources.Load<PoolingManager>("Prefabs/Manager/PoolingManager"), parent);
    }

    private void Awake()
    {
        if (i == null) i = this;
    }

    public void SetBulletPoolingObject(GameObject bulletPrefab)
    {
        bulletPooling.objectToPool = bulletPrefab;
    }

    public GameObject GetBullet()
    {
        return bulletPooling.GetPoolObject();
    }

    public GameObject GetExplode()
    {
        return explodePooling.GetPoolObject();
    }
}
