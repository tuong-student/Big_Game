using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerScripts : BaseCharacter
{
    public PlayerMovement playerMovement;
    public PlayerOncollision playerOncollision;
    public PlayerAnimation playerAnimation;

    [SerializeField] WeaponsHolder weaponsHolder;

    public ParticleSystem dashEff;

    Vector3 movement;
    [SerializeField] GroundGun groundGun = null;

    [SerializeField] GameObject player1View, player2View, player3View;
    public float playerNum = 1;

    public static PlayerScripts Create(Transform parent = null)
    {
        //Create a clone of player object in Resources/Prefabs/Game/Player/Player in Asset folder
        PlayerScripts player = Instantiate(Resources.Load<GameObject>("Prefabs/Game/Player/Player"), parent).GetComponentInChildren<PlayerScripts>();
        return player;
    }

    private void Awake()
    {
        AddScripts();
        player1View.SetActive(false);
        player2View.SetActive(false);
        player3View.SetActive(false);
        switch (playerNum)
        {
            case 1:
                player1View.SetActive(true);
                break;
            case 2:
                player2View.SetActive(true);
                break;
            case 3:
                player3View.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        this.movement = playerMovement.movement;
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetButton("Fire1"))
        {
            weaponsHolder.isShootPress = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            weaponsHolder.isShootPress = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponsHolder.ChangeItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponsHolder.ChangeItem(2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpGun();
        }
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

    void Buy(float amountOfGold, Upgrade upgrade)
    {
        if (GoldManager.i.MinusGold(amountOfGold))
        {
            upgrade.AddStats();
        }
    }

    public void SetGroundGun(GroundGun groundGun)
    {
        this.groundGun = groundGun;
    }

    public void RemoveGroundGun()
    {
        groundGun = null;
    }

    void PickUpGun()
    {
        if (groundGun)
        {
            if (weaponsHolder.SetNewGun(groundGun.GetData()))
            {
                Destroy(groundGun.gameObject);
            }
            else
            {
                //Pick up new gun and drop current gun
                GunData temp = weaponsHolder.GetCurrentGunData();
                weaponsHolder.SetCurrentGunData(groundGun.GetData());
                groundGun.SetData(temp);
            }
        }
    }
}
