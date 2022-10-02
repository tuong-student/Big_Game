using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerScripts : BaseCharacter
{
    public PlayerMovement playerMovement;
    public PlayerOncollision playerOncollision;
    public PlayerAnimation playerAnimation;

    public ParticleSystem dashEff;

    Vector3 movement;

    public static PlayerScripts Create(Transform parent = null)
    {
        //Create a clone of player object in Resources/Prefabs/Game/Player/Player in Asset folder
        return Instantiate<PlayerScripts>(Resources.Load<PlayerScripts>("Prefabs/Game/Player/Player"), parent);
    }

    private void Awake()
    {
        AddScripts();
    }

    private void Update()
    {
        this.movement = playerMovement.movement;

        if(playerAnimation != null)
            SetDirection(this.movement);
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

        if (this.gameObject.GetComponent<PlayerAnimation>() == null)
        {
            this.gameObject.AddComponent<PlayerAnimation>();
        }

        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        playerOncollision = GetComponent<PlayerOncollision>();
    }
    
    void SetDirection(Vector3 movement)
    {
        float x = movement.x;
        float y = movement.y;

        playerAnimation.left = false;
        playerAnimation.right = false;
        playerAnimation.up = false;
        playerAnimation.down = false;
        playerAnimation.stand = true;

        if(x < 0)
        {
            playerAnimation.left = true;
            playerAnimation.stand = false;
        }
        if(x > 0)
        {
            playerAnimation.right = true;
            playerAnimation.stand = false;
        }

        if (y < 0)
        {
            playerAnimation.down = true;
            playerAnimation.stand = false;
        }
        if (y > 0)
        {
            playerAnimation.up = true;
            playerAnimation.stand = false;
        }
    }

    void Buy(float amountOfGold, Upgrade upgrade)
    {
        if (GoldManager.i.MinusGold(amountOfGold))
        {
            upgrade.AddStats();
        }
    }

}
