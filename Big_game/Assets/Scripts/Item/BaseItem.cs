using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NOOD;

public class BaseItem : MonoBehaviour
{
    internal UnityAction action;
    public ItemType itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DetectPlayer");
            PerformAction();
	    }
    }

    public virtual void PerformAction() { }
}

public enum ItemType
{ 
    health,
    mana
}
