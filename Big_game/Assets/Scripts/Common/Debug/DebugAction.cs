using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD.NoodCamera;
using Game.Player;
using Game.Common.Manager;
using Game.UI;
using Game.Common.Interface;

namespace Game.DebugMenu
{
    public class DebugAction : MonoBehaviour, ISingleton
    {
        private bool isInfinityHealth, isInfinityMana;

        private PlayerScripts playerScripts;

        void Awake()
        {
            RegisterToContainer();
        }

        void Start()
        {
            SingletonContainer.Resolve<EventManager>().OnPlayerCreate += EventManager_OnPlayerCreate;
        }

        // Update is called once per frame
        void Update()
        {
            if (isInfinityHealth)
            {
                if (playerScripts.health.value < playerScripts.health.max.value)
                {
                    playerScripts.AddHealth(30);
                }
            }
            if (isInfinityMana)
            {
                if(playerScripts.mana.value < playerScripts.mana.max.value)
                {
                    playerScripts.AddMana(30);
                }
            }
        }

        //--- Perform Action Zone ---//
        private void EventManager_OnPlayerCreate(object sender, EventArgs eventArgs)
        {
            playerScripts = sender as PlayerScripts;
        }

        public void PerformAction(DebugType type)
        {
            Invoke(type.ToString(), 0f);
        }

        public void InfinityHealth()
        {
            isInfinityHealth = !isInfinityHealth;
        }

        public void InfinityMana()
        {
            isInfinityMana = !isInfinityMana;
        }

        public void MinusHealth30()
        {
            playerScripts.Damage(30f);
        }

        public void MinusMana30()
        {
            playerScripts.MinusMana(10f);
        }

        public void AddHealth30()
        {
            playerScripts.AddHealth(30f);
        }

        public void AddMana30()
        {
            playerScripts.AddMana(30f);
        }

        public void AddGold30()
        {
            SingletonContainer.Resolve<GameManager>().AddGold(30);
        }

        public void OpenUpgradePanel()
        {
            SingletonContainer.Resolve<InGameUI>().CreateUpgradePanel();
        }

        public void Save()
        {
            playerScripts.Save();
        }

        public void Load()
        {
            playerScripts.LoadFromSave();
        }

        //-----------------//

        private float oldPlayerFireRate;
        private bool isMaxFireRate = false;
        public void MaxFireRate()
        {
            isMaxFireRate = !isMaxFireRate;
            if (isMaxFireRate)
            {
                oldPlayerFireRate = playerScripts.fireRate.value;
                playerScripts.fireRate.value = 10f;
            }
            else
            {
                playerScripts.fireRate.value = oldPlayerFireRate;
            }
        }

        private float oldPlayerDamage = 0f;
        private bool isMaxDamage = false;
        public void MaxDamage()
        {
            isMaxDamage = !isMaxDamage;
            if(isMaxDamage)
            {
                oldPlayerDamage = playerScripts.damage.value;
                playerScripts.damage.value = 99f;
            }
            else
            {
                playerScripts.damage.value = oldPlayerDamage;
            }
        }

        public void MoveToBossRoom()
        {
            GameObject bossHolder = GameObject.Find("Boss holder");
            playerScripts.transform.position = bossHolder.transform.position + new Vector3(.1f, .1f, 0);
        }

        public void SpawnGun()
        {
            GroundGun groundGun = GroundGun.Create();
            groundGun.transform.position = playerScripts.transform.position + new Vector3(.1f, .1f, 0);
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnregisterToContainer()
        {
            SingletonContainer.UnRegister(this);
        }
        //---------------//
    }
}
