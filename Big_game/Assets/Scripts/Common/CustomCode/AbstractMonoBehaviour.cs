using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NOOD
{
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
            Clear();
            Dispose();
        }

        public void Clear()
        {
            foreach (var obj in objects)
            {
                if (obj)
                {
                    obj.Dispose();
                    DestroyImmediate(obj.gameObject);
                }
            }
        }

        protected virtual void Dispose()
        {
        }
    }
}
