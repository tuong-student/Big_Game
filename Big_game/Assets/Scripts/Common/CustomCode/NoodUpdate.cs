using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOOD
{
    public class NoodUpdate : MonoBehaviour
    {
        public Action PublickUpdateAction;

        private void Update()
        {
            PublickUpdateAction?.Invoke();
        }
    }
}
