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
            if(player.mana.value < player.mana.max.value)
            {
                player.AddMana(manaAmount);
                SingletonContainer.Resolve<AudioManager>().PlaySFX(sound.pickUp);
                Destroy(this.gameObject);
            }
        }
    }
}
