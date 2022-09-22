using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerScripts : BaseCharacter
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerOncollision playerOncollision;

    public static PlayerScripts Create(Transform parent = null)
    {
        //Create a clone of player object in Resources/Prefabs/Game/Player/Player in Asset folder
        return Instantiate<PlayerScripts>(Resources.Load<PlayerScripts>("Prefabs/Game/Player/Player"), parent);
    }

    private void Awake()
    {
        AddScripts();
    }

    void AddScripts()
    {
        if (this.gameObject.GetComponent<PlayerMovement>() == null)
        {
            this.gameObject.AddComponent<PlayerMovement>();
        }

        if (this.gameObject.GetComponent<PlayerOncollision>() == null)
        {
            this.gameObject.AddComponent<PlayerOncollision>();
        }

        playerMovement = GetComponent<PlayerMovement>();
        playerOncollision = GetComponent<PlayerOncollision>();
    }
    
    void Buy(float amountOfGold, Upgrade upgrade)
    {
        if (GoldManager.i.MinusGold(amountOfGold))
        {
            upgrade.AddStats();
        }
    }

}
