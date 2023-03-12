using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common.RPGStats
{

    // May complete this in future
    // My own modifiable value
    [Serializable]
    public class RPGStats<T>
    {
        public Modifiable<T> initial = new Modifiable<T>();
        public PrivateModifiableValue<T> max = new PrivateModifiableValue<T>();
        public PrivateModifiableValue<T> _current = new PrivateModifiableValue<T>();
        public T bonus
        {
            get
            {
                var op = RPGModifider.GetOpType<T>();
                return op.Sum(max.value, op.Negate(initial.value));
            }
        }
        public T value { get { return _current.value; } set
            {
                var cp = RPGModifider.GetCompareType<T>();
                if (cp.IsGreater(value, max.value))
                {
                    _current.Set(max.value);
                }
                else
                {
                    _current.Set(value);
                }
            }
        }
    }

    public class PrivateModifiableValue<T>
    {
        public T modifiedValue = default;
        public T value { get { return modifiedValue; } }


        //--- Operation ---//
        public void Sum(T value)
        {
            var op = RPGModifider.GetOpType<T>();
            modifiedValue = op.Sum(modifiedValue, value);
        }

        public void Times(T value)
        {
            var op = RPGModifider.GetOpType<T>();
            modifiedValue = op.Times(modifiedValue, value);
        }

        public void Divide(T value)
        {
            var op = RPGModifider.GetOpType<T>();
            modifiedValue = op.Divide(modifiedValue, value);
        }

        public void Minus(T value)
        {
            var op = RPGModifider.GetOpType<T>();
            modifiedValue = op.Sum(modifiedValue, op.Negate(value));
        }

        public void Set(T value)
        {
            var op = RPGModifider.GetOpType<T>();
            modifiedValue = op.Set(value);
        }
    }

    public class Modifiable<T>
    {
        public T value { get; set; }
    }
}
