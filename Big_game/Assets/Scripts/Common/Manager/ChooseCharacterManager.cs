using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;
using UnityEngine.UI;

namespace Game.Common.Manager
{
    public class ChooseCharacterManager : MonoBehaviorInstance<ChooseCharacterManager>
    {
        public static ChooseCharacterManager Create(Transform parent = null)
        {
            return Instantiate<ChooseCharacterManager>(Resources.Load<ChooseCharacterManager>("Prefabs/Manager/ChooseCharacterManager.prefab"), parent);
        }

        [SerializeField] Button ConfirmBtn;

        private void Start()
        {
            ConfirmBtn.onClick.AddListener(() =>
            {
                Main.GetInstance.PlayGame();
            });
        }

        private void OnDisable()
        {
            ConfirmBtn.onClick.RemoveAllListeners();
        }
    }
}
