using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common.Interface;

public class SingletonContainer : MonoBehaviour
{
    private static Dictionary<Type, object> _container = new Dictionary<Type, object>();

    public static void Register(object instance)
    {
        Type type = instance.GetType();
        _container[type] = instance;
    }

    public static T Resolve<T>()
    {
        try
        {
            object result = _container[typeof(T)];
            return (T) result;
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
            Debug.Log("No instance of type " + typeof(T) + " found");
            return default;
        }
    }

    public static void UnRegister(object instance)
    {
        Type type = instance.GetType();
        _container.Remove(type);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
