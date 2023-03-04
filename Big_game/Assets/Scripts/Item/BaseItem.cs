using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NOOD;
using Game.Player;

public class BaseItem : MonoBehaviour, IInteractable
{
    internal UnityAction action;
    public ItemType itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DetectPlayer");
            Interact(collision.gameObject.GetComponent<PlayerScripts>());
	    }
    }

    public virtual void Interact(PlayerScripts player)
    {
    }
}

public enum ItemType
{ 
    health,
    mana
}
