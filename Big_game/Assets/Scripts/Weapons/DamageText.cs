using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common.Manager;
using Lean.Transition;

namespace Game.Player.Weapon
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshPro textMeshPro;
        private Color color;

        private void Start()
        {
            MoveUp();
        }

        private void Update()
        {
            color = textMeshPro.color;
            if(color.a > 0)
            {
                color.a -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
            textMeshPro.color = color;
        }

        private void MoveUp()
        {
            this.transform.positionTransition_y(0.5f, 1f,  LeanEase.ExpoOut);
        }
    }
}
