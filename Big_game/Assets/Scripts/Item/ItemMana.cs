using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;

namespace Game.Item
{
    public class ItemMana : BaseItem
    {
        public float manaAmount = 1f;

        public override void Interact(PlayerScripts player)
        {
            if(player.currentMana < player.maxMana && !player.isDead)
            {
                player.AddMana(manaAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
