using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using Game.UI;
using Game.Manager;
using Game.Save;

namespace Game.Player
{
    public class PlayerScripts : BaseCharacter
    {
        #region Component
        public Sprite[] playerSprites;
        public PlayerMovement playerMovement;
        public PlayerOncollision playerOncollision;
        public PlayerAnimation playerAnimation;
        public ParticleSystem dashEff;

        [SerializeField] private WeaponsHolder weaponsHolder;
        [SerializeField] private GameObject player1View, player2View, player3View;
        private IInteractable groundObject = null;
        #endregion

        public int playerNum = 1; // Index of the player sprite (0 -> 2)
        private SaveModels.PlayerModel playerModel; // PlayerModel to save

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
            LoadFromSave();
        }

        private void Start()
        {
            GameInput.OnPlayerInteract += InteractWithObject;

            EventManager.GetInstance.OnPauseGame.OnEventRaise += () =>
            {
                isMoveable = false;
            };
            EventManager.GetInstance.OnContinuewGame.OnEventRaise += () =>
            {
                isMoveable = true;
            };
            EventManager.GetInstance.OnGenerateLevelComplete.OnEventRaise += (int number) =>
            {
                ResetPosition();
            };
        }

        private void Update()
        {
            if (currentHealth <= 0 && isDead == false) Die();
            if (!isMoveable) return;
        }

        private void OnDisable()
        {
            GameInput.OnPlayerInteract -= InteractWithObject;
        }

        void Save()
        {
            playerModel.maxHealth = this.maxHealth;
            playerModel.maxMana = this.maxMana;
            playerModel.criticalRate = this.criticalRate;
            playerModel.dashForce = this.dashForce;
            playerModel.dashTime = this.dashTime;
            playerModel.defence = this.defence;
            playerModel.fireRate = this.fireRate;
            playerModel.playerNum = this.playerNum;
            playerModel.runSpeed = this.runSpeed;
            playerModel.walkSpeed = this.walkSpeed;

            SaveJson.SaveToJson(playerModel, SaveModels.SaveFile.PlayerSave.ToString());
        }

        void LoadFromSave()
        {
            playerModel = LoadJson<SaveModels.PlayerModel>.LoadFromJson(SaveModels.SaveFile.PlayerSave.ToString());
            if (playerModel == null)
            {
                // No save file
                // Create new model, set new data then save
                playerModel = new SaveModels.PlayerModel();
                playerModel.maxHealth = this.maxHealth;
                playerModel.maxMana = this.maxMana;
                playerModel.criticalRate = this.criticalRate;
                playerModel.dashForce = this.dashForce;
                playerModel.dashTime = this.dashTime;
                playerModel.defence = this.defence;
                playerModel.fireRate = this.fireRate;
                playerModel.playerNum = this.playerNum;
                playerModel.runSpeed = this.runSpeed;
                playerModel.walkSpeed = this.walkSpeed;
                Save();
            }
            else
            {
                this.maxHealth = playerModel.maxHealth;
                this.maxMana = playerModel.maxMana;
                this.criticalRate = playerModel.criticalRate;
                this.dashForce = playerModel.dashForce;
                this.dashTime = playerModel.dashTime;
                this.defence = playerModel.defence;
                this.fireRate = playerModel.fireRate;
                this.playerNum = playerModel.playerNum;
                this.runSpeed = playerModel.runSpeed;
                this.walkSpeed = playerModel.walkSpeed;
            }
        }

        public GunData GetCurrentGunData()
        {
            return weaponsHolder.GetCurrentGunData();
        }

        private void AddScripts()
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
            if (GameManager.GetInstance.MinusGold(amountOfGold))
            {
                ApplyUpgrade(upgrade);
                InGameUI.GetInstance.SetStats();
                return true;
            }
            return false;
        }

        private void ApplyUpgrade(Upgrade upgrade)
        {
            //Debug.Log(upgrade.upgradeStats);
            //switch (upgrade.upgradeStats)
            //{
            //    case StatsType.attack:
            //        this.bonusDamage += upgrade.upgradeAmount;
            //        break;
            //    case StatsType.defense:
            //        this.defence += upgrade.upgradeAmount;
            //        break;
            //    case StatsType.mana:
            //        this.maxMana += upgrade.upgradeAmount;
            //        InGameUI.GetInstance.SetMaxMana(maxMana);
            //        break;
            //    case StatsType.maxHealth:
            //        this.maxHealth += upgrade.upgradeAmount;
            //        InGameUI.GetInstance.SetMaxHealth(maxHealth);
            //        break;
            //    case StatsType.movement:
            //        this.runSpeed += upgrade.upgradeAmount;
            //        this.walkSpeed += upgrade.upgradeAmount;
            //        this.currentSpeed = this.runSpeed;
            //        break;
            //    case StatsType.critical:
            //        this.criticalRate += upgrade.upgradeAmount;
            //        break;
            //    case StatsType.fireRate:
            //        this.fireRate += upgrade.upgradeAmount;
            //        break;
            //    case StatsType.reloadSpeed:
            //        this.reloadSpeed += upgrade.upgradeAmount;
            //        break;
            //}
            //LocalDataManager.bonusDamage = bonusDamage;
            //LocalDataManager.defence = defence;
            //LocalDataManager.maxMana = maxMana;
            //LocalDataManager.maxHealth = currentHealth;
            //LocalDataManager.runSpeed = runSpeed;
            //LocalDataManager.walkSpeed = walkSpeed;
            //LocalDataManager.criticalRate = criticalRate;
            //LocalDataManager.bonusFireRate = fireRate;
            //LocalDataManager.bonusReloadSpeed = reloadSpeed;

            //InGameUI.GetInstance.SetStats();
        }

        public void SetGroundObject(IInteractable interactable)
        {
            this.groundObject = interactable;
        }

        public IInteractable GetGroundObject()
        {
            return this.groundObject;
        }

        private void InteractWithObject()
        {
            if(groundObject != null)
            {
                groundObject.Interact(this);
            }
        }

        public void PickUpGun(GroundGun groundGun)
        {
            if (groundGun)
            {
                if (weaponsHolder.SetNewGun(groundGun.GetData()))
                {
                    // after set gun complete destroy groundGun object
                    groundGun.DestroySelf();
                }
                else
                {
                    // Pick up new gun and drop current gun
                    GunData temp = weaponsHolder.GetCurrentGunData();
                    weaponsHolder.ChangeNewGun(groundGun.GetData());
                    groundGun.SetData(temp);
                }
                //AudioManager.GetInstance.PlaySFX(sound.pickUp);
            }
        }

        public override void Die()
        {
            Debug.Log("Player Die");
            playerAnimation.DeadAnimation();
            isDead = true;
            //GameManager.GetInstance.isEndGame = true;
            base.Die();
        }

        private void ResetPosition()
        {
            Vector3 startPos = GameObject.Find("RespawnPos").transform.position;

            this.transform.position = startPos;
        }
    }
}
