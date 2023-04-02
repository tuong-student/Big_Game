using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

namespace Game.Common.Manager
{
    public class ExplodeManager : MonoBehaviour, Game.Common.Interface.ISingleton
    {
        ExplodeType type;
        public List<GameObject> explodes;

        public static ExplodeManager Create(Transform parent = null)
        {
            return Instantiate<ExplodeManager>(Resources.Load<ExplodeManager>("Prefabs/Manager/ExplodeManager"), parent);
        }

        void Awake()
        {
            RegisterToContainer();
        }

        public GameObject GetExplodePref(ExplodeType type)
        {
            switch (type)
            {
                case ExplodeType.Explode:
                    return explodes[0];
                case ExplodeType.ExplodeLaser:
                    return explodes[1];
                case ExplodeType.ExplodeRed:
                    return explodes[2];
                case ExplodeType.ExplodeShotgun:
                    return explodes[3];
                case ExplodeType.ExplodeYellow:
                    return explodes[4];
                default:
                    return explodes[0];
            }
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnregisterToContainer()
        {
            SingletonContainer.UnRegister(this);
        }
    }

    public enum ExplodeType
    {
        Explode,
        ExplodeLaser,
        ExplodeRed,
        ExplodeShotgun,
        ExplodeYellow
    }
}