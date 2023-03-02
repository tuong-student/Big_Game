using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD.NoodCamera;
using Game.Player;

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
                if (PlayerScripts.GetInstance.currentHealth < PlayerScripts.GetInstance.maxHealth)
                {
                    PlayerScripts.GetInstance.AddHealth(30);
                }
            }
            if (isInfinityMana)
            {
                if(PlayerScripts.GetInstance.currentMana < PlayerScripts.GetInstance.maxMana)
                {
                    PlayerScripts.GetInstance.AddMana(30);
                }
            }
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

        private float oldPlayerFireRate;
        private bool isMaxFireRate = false;
        public void MaxFireRate()
        {
            isMaxFireRate = !isMaxFireRate;
            if (isMaxFireRate)
            {
                oldPlayerFireRate = PlayerScripts.GetInstance.fireRate;
                PlayerScripts.GetInstance.fireRate = 10f;
            }
            else
            {
                PlayerScripts.GetInstance.fireRate = oldPlayerFireRate;
            }
        }

        private float oldPlayerDamage;
        private bool isMaxDamage = false;
        public void MaxDamage()
        {
            isMaxDamage = !isMaxDamage;
            if(isMaxDamage)
            {
                oldPlayerDamage = PlayerScripts.GetInstance.playerDamage;
                PlayerScripts.GetInstance.playerDamage = 99f;
            }
            else
            {
                PlayerScripts.GetInstance.playerDamage = oldPlayerDamage;
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
    }
}
