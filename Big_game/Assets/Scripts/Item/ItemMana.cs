using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMana : BaseItem
{
    public float manaAmount = 1f;

    public override void PerformAction()
    {
        if(PlayerScripts.GetInstance.currentMana < PlayerScripts.GetInstance.maxMana)
        { 
            PlayerScripts.GetInstance.AddMana(manaAmount);
            Destroy(this.gameObject);
	    }
    }
}
