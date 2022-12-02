using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviorInstance <T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T GetInstace
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
            }

            if (instance == null) Debug.LogError(typeof(T) + "Not exist");
            return instance;
        }
    }
}
