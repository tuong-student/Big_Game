using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common.Manager;
using DG.Tweening;
using UnityEngine.UI;

namespace Game.Player.Weapon
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshPro textMeshPro;
        private Color color;
        private float moveToY;

        private void Start()
        {
            moveToY = this.transform.position.y + 0.3f;
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
            this.transform.DOLocalMoveY(moveToY, 1f).SetEase(Ease.OutSine);
        }
    }
}
