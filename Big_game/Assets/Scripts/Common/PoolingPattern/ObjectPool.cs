using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectToPool;
    [SerializeField] protected int poolSize = 10;
    public static ObjectPool i;

    private Queue<GameObject> objectQueue = new Queue<GameObject>();

    private Transform spawnTransform;
    private Transform parentTranform;

    private void Awake()
    {
        if (i == null) i = this;
    }

    public GameObject GetPoolObject()
    {
        CreateParentObjectIfNeed();

        GameObject spawnObj = null;

        if(objectQueue.Count < poolSize)
        {
            spawnObj = Instantiate(objectToPool, null);
            if (spawnTransform)
                spawnObj.transform.position = spawnTransform.position;
            spawnObj.transform.SetParent(parentTranform);
        }
        else
        {
            spawnObj = objectQueue.Dequeue();
            if (!spawnObj.Equals(objectToPool))
            {
                Destroy(spawnObj.gameObject);
                spawnObj = Instantiate(objectToPool, null);
                spawnObj.transform.SetParent(parentTranform);
            }
            if(spawnTransform)
                spawnObj.transform.position = spawnTransform.position;
            spawnObj.SetActive(true);
        }

        objectQueue.Enqueue(spawnObj);
        return spawnObj;
    }

    void CreateParentObjectIfNeed()
    {
        if(parentTranform == null)
        {
            if(parentTranform == null)
            {
                parentTranform = Instantiate(new GameObject("_ObjectPool")).transform;
            }
        }
    }
}
