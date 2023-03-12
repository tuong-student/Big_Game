using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NOOD;

namespace Game.Common.Manager
{
    public class DamageTextManager : MonoBehaviorInstance<DamageTextManager>
    {
        private GameObject damageTextObject;

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
            PoolingManager.GetInstance.SetDamageTextPoolingObject(damageTextObject);
            TextMeshPro damageText = PoolingManager.GetInstance.GetDamageText().GetComponent<TextMeshPro>();
            SetDamageText(damageText, damage);
            return damageText.gameObject;
        }

        private void SetDamageText(TextMeshPro damageText, float damage)
        {
            damageText.text = damage.ToString("0");
        }
    }
}
