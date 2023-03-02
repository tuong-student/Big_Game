using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class Store : MonoBehaviour, IInteractable
    {
        bool isBuyable;
        
        public void Interact()
        {
            Debug.Log("Store Interact");
            GameCanvas.GetInstance.CreateUpgradePanel();
        }
    }
}
