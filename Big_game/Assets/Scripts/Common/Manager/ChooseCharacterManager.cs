using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;
using UnityEngine.UI;
using Game.Common.Interface;

namespace Game.Common.Manager
{
    public class ChooseCharacterManager : MonoBehaviour, ISingleton
    {
        public static ChooseCharacterManager Create(Transform parent = null)
        {
            return Instantiate<ChooseCharacterManager>(Resources.Load<ChooseCharacterManager>("Prefabs/Manager/ChooseCharacterManager.prefab"), parent);
        }

        [SerializeField] Button ConfirmBtn;
        void Awake()
        {
            RegisterToContainer();
        }

        private void Start()
        {
            ConfirmBtn.onClick.AddListener(() =>
            {
                //Main.GetInstance.PlayGame();
            });
        }

        private void OnDisable()
        {
            ConfirmBtn.onClick.RemoveAllListeners();
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
}
