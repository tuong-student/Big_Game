using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractMonoBehaviour : MonoBehaviour
{
    internal List<AbstractMonoBehaviour> objects = new List<AbstractMonoBehaviour>();

    public AbstractMonoBehaviour AddTo(AbstractMonoBehaviour parent)
    {
        parent.objects.Add(this);
        return this;
    }

    private void OnDestroy()
    {
        foreach(var obj in objects)
        {
            if (obj)
            {
                Destroy(obj.gameObject);
            }
        }
    }

    public void Clear()
    {
        foreach (var obj in objects)
        {
            if (obj)
            {
                Destroy(obj.gameObject);
            }
        }
    }
}
