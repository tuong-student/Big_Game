using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;

namespace Game.UI
{
    public class Store : MonoBehaviour, IInteractable
    {
        private bool isBuyable;
        
        public void Interact(PlayerScripts player)
        {
            Debug.Log("Store Interact");
            InGameUI.GetInstance.CreateUpgradePanel();
        }
    }
}
