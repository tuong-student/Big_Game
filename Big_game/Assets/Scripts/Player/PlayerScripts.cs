using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerScripts : BaseCharacter
{
    #region Component
    public PlayerMovement playerMovement;
    public PlayerOncollision playerOncollision;
    public PlayerAnimation playerAnimation;
    public ParticleSystem dashEff;

    [SerializeField] WeaponsHolder weaponsHolder;
    GroundGun groundGun = null;
    [SerializeField] GameObject player1View, player2View, player3View;
    #endregion

    public float playerNum = 1; //Index of the player sprite (0 -> 2)
    Vector3 movement;

    #region Bool

    #endregion

    public static PlayerScripts Create(Transform parent = null)
    {
        //Create a clone of player object in Resources/Prefabs/Game/Player/Player in Asset folder
        PlayerScripts player = Instantiate(Resources.Load<GameObject>("Prefabs/Game/Player/Player"), parent).GetComponentInChildren<PlayerScripts>();
        return player;
    }

    public static PlayerScripts GetInstance { get { return (PlayerScripts)Instance; } private set { } }

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

    private void Start()
    {
        LocalDataManager.health = health;
        InGameUI.GetInstace.SetHealth(LocalDataManager.health);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(30);
        }
        this.movement = playerMovement.movement;
        if (health <= 0 && isDead == false) Die();
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

    public bool Buy(int amountOfGold, Upgrade upgrade)
    {
        if (GoldManager.GetInstace.MinusGold(amountOfGold))
        {
            ApplyUpgrade(upgrade);
            return true;
        }
        return false;
    }

    void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.upgradeStats)
        {
            case StatsType.attack:
                this.damage += upgrade.upgradeAmount; 
                break;
            case StatsType.defense:
                this.defence += upgrade.upgradeAmount;
                break;
            case StatsType.mana:
                this.mana += upgrade.upgradeAmount;
                break;
            case StatsType.health:
                this.health += upgrade.upgradeAmount;
                break;
            case StatsType.movement:
                this.runSpeed += upgrade.upgradeAmount;
                this.walkSpeed += upgrade.upgradeAmount;
                break;
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
                weaponsHolder.ChangeNewGun(groundGun.GetData());
                groundGun.SetData(temp);
            }
        }
    }

    public override void Die()
    {
        Debug.Log("Player Die");
        playerAnimation.DeadAnimation();
        isDead = true;
        GameManager.GetInstace.isEndGame = true;
    }
}
