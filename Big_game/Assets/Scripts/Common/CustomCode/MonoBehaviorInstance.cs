using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NOOD
{
    public class MonoBehaviorInstance <T> : AbstractMonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T GetInstance
        {
            get
            {
                if(instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                }

                if (instance == null)
                {
                    Debug.LogWarning("Errorrrrr: " + typeof(T) + " not exit");
                }
                return instance;
            }
        }
    }
}

