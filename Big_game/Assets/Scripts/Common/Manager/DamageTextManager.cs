using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NOOD;

namespace Game.Common.Manager
{
    public class DamageTextManager : MonoBehaviour, Game.Common.Interface.ISingleton
    {
        private GameObject damageTextObject;
        private PoolingManager poolingManager;

        void Start()
        {
            RegisterToContainer();
            poolingManager = SingletonContainer.Resolve<PoolingManager>();
        }

        public GameObject CreateDamageText(float damage, bool isCritical)
        {
            if (isCritical)
            {
                damageTextObject = Resources.Load<GameObject>("Prefabs/Game/Text/DamageText-Critical");
            }
            else
            {
                damageTextObject = Resources.Load<GameObject>("Prefabs/Game/Text/DamageText");
            }
            poolingManager.SetDamageTextPoolingObject(damageTextObject);
            TextMeshPro damageText = poolingManager.GetDamageText().GetComponent<TextMeshPro>();
            SetDamageText(damageText, damage);
            return damageText.gameObject;
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnregisterToContainer()
        {
            SingletonContainer.UnRegister(this);
        }

        private void SetDamageText(TextMeshPro damageText, float damage)
        {
            damageText.text = damage.ToString("0");
        }
    }
}
