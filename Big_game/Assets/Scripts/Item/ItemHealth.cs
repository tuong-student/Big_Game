using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Game.Player;

namespace Game.Item
{
    public class ItemHealth : BaseItem 
    {
        public float healthAmount = 1f;

        public override void PerformAction() 
        {
         //   if(PlayerScripts.GetInstance.currentHealth > 0 || PlayerScripts.GetInstance.currentHealth < PlayerScripts.GetInstance.maxHealth)
         //   { 
         //       PlayerScripts.GetInstance.AddHealth(healthAmount);
         //       Destroy(this.gameObject);
	        //}
        }
    }
}
