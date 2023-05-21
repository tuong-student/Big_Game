using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Base;
using Game.UI.Support;
using Game.Common.Manager;
using Game.Save;
using Game.Player.Weapon;
using Game.Common.RPGStats;
using NOOD;

namespace Game.Player
{
    public class PlayerScripts : AbstractMonoBehaviour, Game.Common.Interface.ISingleton
    {
        #region Event
        public EventHandler<OnPlayerStatsChangeEventArg> OnPlayerStatsChange;
        public EventHandler<OnHealthChangeEventArgs> OnHealthChange;
        public class OnHealthChangeEventArgs : EventArgs
        {
            public float health;
            public float maxHealth;
        }
        public EventHandler<OnManaChangeEventArgs> OnManaChange;
        public class OnManaChangeEventArgs : EventArgs
        {
            public float mana;
            public float maxMana;
        }
        public class OnPlayerStatsChangeEventArg : EventArgs
        {
            public float maxHealth;
            public float bonusHealth;
            public float maxMana;
            public float bonusMana;
            public float damage;
            public float bonusDamage;
            public float criticalRate;
            public float bonusCriticalRate;
            public float fireRate;
            public float bonusFireRate;
            public float currentSpeed;
            public float bonusSpeed;
            public float defense;
            public float bonusDefense;
        }
        public Action OnPlayerDead;
        #endregion

        #region Component
        public Sprite[] playerSprites;
        public PlayerMovement playerMovement;
        public PlayerOncollision playerOncollision;
        public PlayerAnimation playerAnimation;
        public ParticleSystem dashEff;

        [SerializeField] private WeaponsHolder weaponsHolder;
        [SerializeField] private List<GameObject> playerViewList;
        private IInteractable groundObject = null;
        #endregion

        #region Stats
        public float baseHealth = 100f;
        public float baseMana = 50f;
        public float baseCriticalRate = 1f;
        public float baseFireRate = 0f;
        public float baseDamage = 1f;
        public float baseSpeed = 0.8f;
        public float baseDefense = 0f;

        public RPGStats<float> health = new RPGStats<float>();
        public RPGStats<float> mana = new RPGStats<float>();
        public RPGStats<float> damage = new RPGStats<float>();
        public RPGStats<float> criticalRate = new RPGStats<float>();
        public RPGStats<float> fireRate = new RPGStats<float>();
        public RPGStats<float> speed = new RPGStats<float>();
        public RPGStats<float> defense = new RPGStats<float>();

        public float dashForce = 30f;
        public float dashTime = 0.5f;

        public RPGStats<float> temp = new RPGStats<float>();

        #endregion

        public int playerNum = 1; // Index of the player sprite (0 -> 2)
        private SaveModels.PlayerModel playerModel; // PlayerModel to save

        #region Bool
        private bool isMoveable = true;
        public bool IsMoveable { get { return isMoveable; } }
        public bool isDead { get; private set; }
        #endregion

        private EventManager eventManager;

        public static PlayerScripts Create(Transform parent = null)
        {
            //Create a clone of player object in Resources/Prefabs/Game/Player/Player in Asset folder
            PlayerScripts player = Instantiate<PlayerScripts>(Resources.Load<PlayerScripts>("Prefabs/Game/Player/Player"), parent);
            return player;
        }

        private void Awake()
        {
            RegisterToContainer();
            this.health.initial.value = baseHealth;
            this.mana.initial.value = baseMana;
            this.damage.initial.value = baseDamage;
            this.criticalRate.initial.value = baseCriticalRate;
            this.fireRate.initial.value = baseFireRate;
            this.speed.initial.value = baseSpeed;
            this.defense.initial.value = baseDefense;

            LoadFromSave();
        }

        private void Start()
        {
            eventManager = SingletonContainer.Resolve<EventManager>();

            eventManager.OnPlayerCreate?.Invoke(this, EventArgs.Empty);
            this.AddTo(eventManager);

            GameInput.OnPlayerInteract += InteractWithObject;

            eventManager.OnPauseGame.OnEventRaise += CanNotMove;

            eventManager.OnContinueGame.OnEventRaise += CanMove;
            eventManager.OnContinueGame.OnEventRaise += UpdatePlayerSprite;
            eventManager.OnContinueGame.OnEventRaise += ActiveOnHealthChange;
            eventManager.OnContinueGame.OnEventRaise += ActiveOnManaChange;
            eventManager.OnGenerateLevel.OnEventRaise += Save;

            eventManager.OnGenerateLevelComplete.OnEventRaise += (int number) =>
            {
                NoodyCustomCode.StartDelayFunction(() => ResetPosition(0), 0.2f);
            };

            UpdatePlayerSprite();
            ActiveOnHealthChange();
            ActiveOnManaChange();
        }

        private void Update()
        {
            if (!isMoveable) return;
        }

        protected override void Dispose()
        {

        }

        public void ChangePlayerVisualWithPlayerNum(int playerNum)
        {
            this.playerNum = playerNum;
            for(int i = 0; i < playerViewList.Count; i++)
            {
                if(i == playerNum - 1)
                {
                    playerViewList[i].SetActive(true);
                }
                else
                {
                    playerViewList[i].SetActive(false);
                }
            }
            playerAnimation.GetAnimAndSrAgain();
        }

        private void CanMove()
        {
            isMoveable = true;
        }

        private void CanNotMove()
        {
            isMoveable = false;
        }

        private void UpdatePlayerSprite()
        {
            SingletonContainer.Resolve<SupportUIComponentHolder>().playerSprite = playerSprites[playerNum - 1];
        }

        public void Save()
        {
            playerModel.maxHealth = this.health.max.value;
            playerModel.maxMana = this.mana.max.value;
            playerModel.criticalRate = this.criticalRate.value;
            playerModel.dashForce = this.dashForce;
            playerModel.dashTime = this.dashTime;
            playerModel.defense = this.defense.value;
            playerModel.fireRate = this.fireRate.value;
            playerModel.speed = this.speed.value;

        }

        public void LoadFromSave()
        {
            playerModel = LoadJson<SaveModels.PlayerModel>.LoadFromJson(SaveModels.SaveFile.PlayerSave.ToString());

            if (playerModel == null)
            {
                // No save file
                // Create new model, set new data then save
                playerModel = new SaveModels.PlayerModel();
                playerModel.maxHealth = this.health.initial.value;
                playerModel.maxMana = this.mana.initial.value;
                playerModel.criticalRate = this.criticalRate.initial.value;
                playerModel.damage = this.damage.initial.value;
                playerModel.dashForce = this.dashForce;
                playerModel.dashTime = this.dashTime;
                playerModel.defense = this.defense.initial.value;
                playerModel.fireRate = this.fireRate.initial.value;
                playerModel.speed = this.speed.initial.value;
            }


            this.health.max.Set(playerModel.maxHealth);
            this.health.value = this.health.max.value;
            this.mana.max.Set(playerModel.maxMana);
            this.mana.value = this.mana.max.value;
            this.criticalRate.max.Set(playerModel.criticalRate);
            this.criticalRate.value = playerModel.criticalRate;
            this.damage.max.Set(playerModel.damage);
            this.damage.value = playerModel.damage;
            this.dashForce = playerModel.dashForce;
            this.dashTime = playerModel.dashTime;
            this.defense.max.Set(playerModel.defense);
            this.defense.value = playerModel.defense;
            this.fireRate.max.Set(playerModel.fireRate);
            this.fireRate.value = playerModel.fireRate;
            this.speed.max.Set(playerModel.speed);
            this.speed.value = playerModel.speed;

            Save();
        }

        public SaveModels.PlayerModel GetPlayerModel()
        {
            return playerModel;
        }

        public void AddHealth(float amount)
        {
            this.health.value += amount;
            ActiveOnHealthChange();
        }

        public void MinusHealth(float amount)
        {
            this.health.value -= amount;
            ActiveOnHealthChange();
        }

        public void AddMana(float amount)
        {
            this.mana.value += amount;
            ActiveOnManaChange();
        }

        public void MinusMana(float amount)
        {
            this.mana.value -= amount;
            ActiveOnManaChange();
        }

        private void ActiveOnHealthChange()
        {
            OnHealthChange?.Invoke(this, new OnHealthChangeEventArgs
            {
                health = this.health.value,
                maxHealth = this.health.max.value
            });
        }

        public void ActiveOnManaChange()
        {
            OnManaChange?.Invoke(this, new OnManaChangeEventArgs
            {
                mana = this.mana.value,
                maxMana = this.mana.max.value
            });
        }

        public void ActiveOnPlayerStatsChange()
        {
            OnPlayerStatsChange?.Invoke(this, new OnPlayerStatsChangeEventArg
            {
                maxHealth = this.health.max.value,
                bonusHealth = this.health.bonus,
                maxMana = this.mana.max.value,
                bonusMana = this.mana.bonus,
                damage = this.GetCurrentGunData().damage + this.damage.value,
                bonusDamage = this.damage.value,
                criticalRate = this.criticalRate.value,
                bonusCriticalRate = this.criticalRate.bonus,
                fireRate = this.GetCurrentGunData().fireRate + this.fireRate.value,
                bonusFireRate = this.fireRate.value,
                currentSpeed = this.speed.value,
                bonusSpeed = this.speed.bonus,
                defense = this.defense.value,
                bonusDefense = this.defense.bonus
            });
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
            if (SingletonContainer.Resolve<GameManager>().MinusGold(amountOfGold))
            {
                ApplyUpgrade(upgrade);
                return true;
            }
            return false;
        }

        private void ApplyUpgrade(Upgrade upgrade)
        {
            Debug.Log(upgrade.upgradeStats);
            switch (upgrade.upgradeStats)
            {
                case StatsType.attack:
                    this.damage.max.Sum(upgrade.upgradeAmount);
                    this.damage.value += upgrade.upgradeAmount;
                    break;
                case StatsType.defense:
                    this.defense.max.Sum(upgrade.upgradeAmount);
                    this.defense.value += upgrade.upgradeAmount;
                    break;
                case StatsType.maxMana:
                    this.mana.max.Sum(upgrade.upgradeAmount);
                    break;
                case StatsType.maxHealth:
                    this.health.max.Sum(upgrade.upgradeAmount);
                    break;
                case StatsType.movement:
                    this.speed.max.Sum(upgrade.upgradeAmount);
                    this.speed.value += upgrade.upgradeAmount;
                    break;
                case StatsType.criticalRate:
                    this.criticalRate.max.Sum(upgrade.upgradeAmount);
                    this.criticalRate.value += upgrade.upgradeAmount;
                    break;
                case StatsType.fireRate:
                    this.fireRate.max.Sum(upgrade.upgradeAmount);
                    this.fireRate.value += upgrade.upgradeAmount;
                    break;
            }
            ActiveOnPlayerStatsChange();
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
                weaponsHolder.PickupNewGun(groundGun);
            }
        }

        public void Die()
        {
            Debug.Log("Player Die");
            playerAnimation.DeadAnimation();
            isDead = true;
            OnPlayerDead?.Invoke();
            SingletonContainer.Resolve<EventManager>().OnLoseGame.RaiseEvent();
        }

        public void ResetPosition(int number)
        {
            Vector3 startPos = GameObject.Find("RespawnPos").transform.position;

            this.transform.position = startPos;
        }

        public void Damage(float value)
        {
            this.health.value -= value;
            ActiveOnHealthChange();
            NOOD.NoodCamera.CameraShake.GetInstance.HeaveShake();
            if(this.health.value <= 0)
            {
                Die();
            }
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnregisterToContainer()
        {
            SingletonContainer.UnRegister(this);
        }

        public void Revive()
        {
            this.isMoveable = true;
            this.isDead = false;
            this.playerAnimation.Revive();
            this.health.value = this.health.max.value;
            this.mana.value = this.mana.max.value;
            ActiveOnManaChange();
            ActiveOnHealthChange();
        }
    }
}
