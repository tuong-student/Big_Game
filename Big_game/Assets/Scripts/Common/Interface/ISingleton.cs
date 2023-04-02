using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common.Interface
{
    public interface ISingleton 
    {
        void RegisterToContainer();
        void UnregisterToContainer();
    }
}
