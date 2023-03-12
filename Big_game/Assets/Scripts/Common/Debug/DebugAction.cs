using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD.NoodCamera;
using Game.Player;
using Game.Common.Manager;
using Game.UI;

namespace Game.DebugMenu
{
    public class DebugAction : MonoBehaviour
    {
        private bool isInfinityHealth, isInfinityMana;

        // Update is called once per frame
        void Update()
        {
            if (isInfinityHealth)
            {
                if (PlayerScripts.GetInstance.health.value < PlayerScripts.GetInstance.health.max.value)
                {
                    PlayerScripts.GetInstance.AddHealth(30);
                }
            }
            if (isInfinityMana)
            {
                if(PlayerScripts.GetInstance.mana.value < PlayerScripts.GetInstance.mana.max.value)
                {
                    PlayerScripts.GetInstance.AddMana(30);
                }
            }
        }

        //--- Perform Action Zone ---//
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
            PlayerScripts.GetInstance.Damage(30f);
        }

        public void MinusMana30()
        {
            PlayerScripts.GetInstance.MinusMana(10f);
        }

        public void AddHealth30()
        {
            PlayerScripts.GetInstance.AddHealth(30f);
        }

        public void AddMana30()
        {
            PlayerScripts.GetInstance.AddMana(30f);
        }

        public void AddGold30()
        {
            GameManager.GetInstance.AddGold(30);
        }

        public void OpenUpgradePanel()
        {
            InGameUI.GetInstance.CreateUpgradePanel();
        }

        public void Save()
        {
            PlayerScripts.GetInstance.Save();
        }

        public void Load()
        {
            PlayerScripts.GetInstance.LoadFromSave();
        }

        //-----------------//

        private float oldPlayerFireRate;
        private bool isMaxFireRate = false;
        public void MaxFireRate()
        {
            isMaxFireRate = !isMaxFireRate;
            if (isMaxFireRate)
            {
                oldPlayerFireRate = PlayerScripts.GetInstance.fireRate.value;
                PlayerScripts.GetInstance.fireRate.value = 10f;
            }
            else
            {
                PlayerScripts.GetInstance.fireRate.value = oldPlayerFireRate;
            }
        }

        private float oldPlayerDamage = 0f;
        private bool isMaxDamage = false;
        public void MaxDamage()
        {
            isMaxDamage = !isMaxDamage;
            if(isMaxDamage)
            {
                oldPlayerDamage = PlayerScripts.GetInstance.damage.value;
                PlayerScripts.GetInstance.damage.value = 99f;
            }
            else
            {
                PlayerScripts.GetInstance.damage.value = oldPlayerDamage;
            }
        }

        public void MoveToBossRoom()
        {
            GameObject bossHolder = GameObject.Find("Boss holder");
            PlayerScripts.GetInstance.transform.position = bossHolder.transform.position + new Vector3(.1f, .1f, 0);
        }

        public void SpawnGun()
        {
            GroundGun groundGun = GroundGun.Create();
            groundGun.transform.position = PlayerScripts.GetInstance.transform.position + new Vector3(.1f, .1f, 0);
        }
        //---------------//
    }
}
