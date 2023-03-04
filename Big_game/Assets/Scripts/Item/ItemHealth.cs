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

        public override void Interact(PlayerScripts player) 
        {
            if (player.currentHealth < player.maxHealth && !player.isDead)
            {
                player.AddHealth(healthAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
