using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerScripts : BaseCharacter
{
    #region Component
    public Sprite[] playerSprites;
    public PlayerMovement playerMovement;
    public PlayerOncollision playerOncollision;
    public PlayerAnimation playerAnimation;
    public ParticleSystem dashEff;

    [SerializeField] WeaponsHolder weaponsHolder;
    [SerializeField] GameObject player1View, player2View, player3View;
    #endregion

    public int playerNum = 1; //Index of the player sprite (0 -> 2)

    #region Bool
    private bool isMoveable = true;
    public bool IsMoveable { get { return isMoveable; } }
    #endregion

    public static PlayerScripts Create(Transform parent = null)
    {
        //Create a clone of player object in Resources/Prefabs/Game/Player/Player in Asset folder
        PlayerScripts player = Instantiate<PlayerScripts>(Resources.Load<PlayerScripts>("Prefabs/Game/Player/Player"), parent);
        return player;
    }

    public static PlayerScripts GetInstance { get { return (PlayerScripts)Instance; } private set { } }

    private void Awake()
    {
        AddScripts();
        playerNum = LocalDataManager.playerNumber;
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

        NOOD.NoodyCustomCode.StartDelayFunction(() => { InGameUI.GetInstance.ChangePlayerSprite(playerSprites[playerNum - 1]); }, 0.05f);
    }

    private void Start()
    {
        LoadFromSave();
        EventManager.GetInstance.OnNewGame.OnEventRaise += () =>
        {
            if (this) Destroy(this.gameObject, 1f);
        };
        EventManager.GetInstance.OnContinuewGame.OnEventRaise += () =>
        {
            isMoveable = true;
        };
        EventManager.GetInstance.OnTurnOnUI.OnEventRaise += () =>
        {
            isMoveable = false;
        };
        EventManager.GetInstance.OnGenerateLevel.OnEventRaise += () =>
        {
            isMoveable = false;
        };
        EventManager.GetInstance.OnGenerateLevelComplete.OnEventRaise += () =>
        {
            isMoveable = true;
        };

        InGameUI.GetInstance.SetHealth(maxHealth);
        InGameUI.GetInstance.SetMana(maxMana);
    }

    private void Update()
    {
        if (!isMoveable) return;
        if (currentHealth <= 0 && isDead == false) Die();
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
            weaponsHolder.ChangeGun(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponsHolder.ChangeGun(2);
        }
    }

    void LoadFromSave()
    {
        bonusDamage = LocalDataManager.bonusDamage;
        defence = LocalDataManager.defence;
        maxHealth = LocalDataManager.maxHealth;
        maxMana = LocalDataManager.maxMana;
        runSpeed = LocalDataManager.runSpeed;
        walkSpeed = LocalDataManager.walkSpeed;
        currentSpeed = runSpeed;
        criticalRate = LocalDataManager.criticalRate;
        fireRate = LocalDataManager.bonusFireRate;
        reloadSpeed = LocalDataManager.bonusReloadSpeed;
    }

    public GunData GetCurrentGunData()
    {
        return weaponsHolder.GetCurrentGunData();
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
        if (GoldManager.GetInstance.MinusGold(amountOfGold))
        {
            ApplyUpgrade(upgrade);
            InGameUI.GetInstance.SetStats();
            return true;
        }
        return false;
    }

    void ApplyUpgrade(Upgrade upgrade)
    {
        Debug.Log(upgrade.upgradeStats);
        switch (upgrade.upgradeStats)
        {
            case StatsType.attack:
                this.bonusDamage += upgrade.upgradeAmount;
                break;
            case StatsType.defense:
                this.defence += upgrade.upgradeAmount;
                break;
            case StatsType.mana:
                this.maxMana += upgrade.upgradeAmount;
                InGameUI.GetInstance.SetMaxMana(maxMana);
                break;
            case StatsType.maxHealth:
                this.maxHealth += upgrade.upgradeAmount;
                InGameUI.GetInstance.SetMaxHealth(maxHealth);
                break;
            case StatsType.movement:
                this.runSpeed += upgrade.upgradeAmount;
                this.walkSpeed += upgrade.upgradeAmount;
                this.currentSpeed = this.runSpeed;
                break;
            case StatsType.critical:
                this.criticalRate += upgrade.upgradeAmount;
                break;
            case StatsType.fireRate:
                this.fireRate += upgrade.upgradeAmount;
                break;
            case StatsType.reloadSpeed:
                this.reloadSpeed += upgrade.upgradeAmount;
                break;
        }
        LocalDataManager.bonusDamage = bonusDamage;
        LocalDataManager.defence = defence;
        LocalDataManager.maxMana = maxMana;
        LocalDataManager.maxHealth = currentHealth;
        LocalDataManager.runSpeed = runSpeed;
        LocalDataManager.walkSpeed = walkSpeed;
        LocalDataManager.criticalRate = criticalRate;
        LocalDataManager.bonusFireRate = fireRate;
        LocalDataManager.bonusReloadSpeed = reloadSpeed;

        InGameUI.GetInstance.SetStats();
    }

    public void PickUpGun(GroundGun groundGun)
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
            AudioManager.GetInstance.PlaySFX(sound.pickUp);
        }
    }

    public override void Die()
    {
        Debug.Log("Player Die");
        playerAnimation.DeadAnimation();
        isDead = true;
        GameManager.GetInstance.isEndGame = true;
        EventManager.GetInstance.OnLoseGame.RaiseEvent();
        base.Die();
    }

    private void Reset()
    {
        this.maxHealth = 100f;
        this.maxMana = 50f;
        this.defence = 0f;
        this.walkSpeed = 0.5f;
        this.runSpeed = 0.8f;
        this.criticalRate = 0.5f;
        this.bonusDamage = 0f;
        this.reloadSpeed = 0;
        this.fireRate = 0;

    }
}
