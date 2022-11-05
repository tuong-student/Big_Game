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

    public static PlayerScripts Create(Transform parent = null)
    {
        //Create a clone of player object in Resources/Prefabs/Game/Player/Player in Asset folder
        PlayerScripts player = Instantiate(Resources.Load<GameObject>("Prefabs/Game/Player/Player"), parent).GetComponentInChildren<PlayerScripts>();
        return player;
    }

    private void Awake()
    {
        AddScripts();
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
