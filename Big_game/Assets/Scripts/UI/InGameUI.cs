using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NOOD;
using Game.Player;
using Game.Player.Weapon;
using Game.UI.Support;
using Game.Common.Manager;

namespace Game.UI
{
    public class InGameUI : AbstractMonoBehaviour, Game.Common.Interface.ISingleton
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider manaSlider;
        [SerializeField] private Image healthWarning, manaWarning;
        [SerializeField] private StatsMenuUI statsMenuUI;
        [SerializeField] private RectTransform statsMenuRect;
        [SerializeField] private Image mainGun, subGun, playerImage;
        [SerializeField] private GameCanvas gameCanvas;
        [SerializeField] private Text goldText;
        
        private bool isStatsShow = false;

        public float maxHealth = 100;

        public float maxMana = 50;
        public float currentMana;
        private int currentGunNumber = 1;

        private EventManager eventManager;
        private GameManager gameManager;
        private SupportUIComponentHolder supportUIComponent;

        private void Awake()
        {
            RegisterToContainer();   
            gameCanvas = GetComponentInParent<GameCanvas>();
        }

        public void OnEnable()
        {
            UpdateUIIfEnable();
        }

        public void Start()
        {
            eventManager = SingletonContainer.Resolve<EventManager>();
            gameManager = SingletonContainer.Resolve<GameManager>();
            eventManager.OnPlayerCreate += EventManager_OnPlayerCreate;
            
            UpdateUIIfEnable();
            GameInput.OnPlayerWatchStats += ShowStatsPressed;
            GameInput.OnPlayerChangeGun += UpdateGunSprite;
            WeaponsHolder.OnPickUpGun += WeaponsHolder_OnPickUpGun;
            gameManager.OnGoldChange += UpdateGoldText;

            supportUIComponent.OnUpdateHealth += SupportUIComponentHolder_OnUpdateHealth;
            supportUIComponent.OnUpdateMana += SupportUIComponentHolder_OnUpdateMana;
            SingletonContainer.Resolve<EventManager>().OnPlayerCreate += (object sender, EventArgs args) => UpdateUIIfEnable();

            this.AddTo(gameManager);

            StartCoroutine(WarningAnimate());
        }


        private void OnDisable()
        {
            
        }

        protected override void Dispose()
        {
            if(supportUIComponent == null) return;
            supportUIComponent.OnUpdateHealth -= SupportUIComponentHolder_OnUpdateHealth;
            supportUIComponent.OnUpdateMana -= SupportUIComponentHolder_OnUpdateMana;
        }

        #region EventFunction
        private void EventManager_OnPlayerCreate(object sender, EventArgs eventArgs)
        {
            PlayerScripts player = sender as PlayerScripts;

            player.OnHealthChange += PlayerScripts_OnHealthChange;
            player.OnManaChange += PlayerScripts_OnManaChange;
        }

        public void WeaponsHolder_OnPickUpGun(object sender, EventArgs eventArgs)
        {
            UpdateGunSprite(1);
        }

        public void PlayerScripts_OnHealthChange(object sender, PlayerScripts.OnHealthChangeEventArgs eventArgs)
        {
            this.SetHealth(eventArgs.health, eventArgs.maxHealth);
        }

        public void PlayerScripts_OnManaChange(object sender, PlayerScripts.OnManaChangeEventArgs eventArgs)
        {
            this.SetMana(eventArgs.mana, eventArgs.maxMana);
        }

        public void SupportUIComponentHolder_OnUpdateHealth(object sender, PlayerScripts.OnHealthChangeEventArgs eventArgs)
        {
            SetHealth(eventArgs.health, eventArgs.maxHealth);
        }

        public void SupportUIComponentHolder_OnUpdateMana(object sender, PlayerScripts.OnManaChangeEventArgs eventArgs)
        {
            SetMana(eventArgs.mana, eventArgs.maxMana);
        }
        #endregion

        Color color = new Color(1, 1, 1, 1);
        private IEnumerator WarningAnimate()
        {
            while(true)
            {
                if(healthWarning.isActiveAndEnabled == false && manaWarning.isActiveAndEnabled == false) yield return null;

                // if(healthWarning.isActiveAndEnabled)
                //     color = healthWarning.color;
                // if(manaWarning.isActiveAndEnabled)
                // {
                //     Debug.Log(manaWarning.color);
                //     color = manaWarning.color;
                // }
                
                // Fade down color
                while(color.a > 0)
                {
                    yield return null;
                    color.a -= Time.deltaTime;
                    if(healthWarning.isActiveAndEnabled)
                        healthWarning.color = color;
                    if(manaWarning.isActiveAndEnabled)
                        manaWarning.color = color;
                }
                // Fade up color
                while(color.a < 1)
                {
                    yield return null;
                    color.a += Time.deltaTime;
                    if(healthWarning.isActiveAndEnabled)
                        healthWarning.color = color;
                    if(manaWarning.isActiveAndEnabled)
                        manaWarning.color = color;
                }
            }
        }

        private void UpdateUIIfEnable()
        {
            supportUIComponent = SingletonContainer.Resolve<SupportUIComponentHolder>();
            if(supportUIComponent == null) return;
            NoodyCustomCode.StartDelayFunction(() =>
            {
                UpdatePlayerSprite();
                UpdateGunSprite();
            }, 0.2f);

            if(supportUIComponent != null)
            {
                SupportUIComponentHolder_OnUpdateHealth(null, supportUIComponent.OnPlayerHealthChangeEventArgs);
                SupportUIComponentHolder_OnUpdateMana(null, supportUIComponent.OnPlayerManaChangeEventArgs);
                supportUIComponent.OnUpdateHealth += SupportUIComponentHolder_OnUpdateHealth;
                supportUIComponent.OnUpdateMana += SupportUIComponentHolder_OnUpdateMana;
            } 
        }

        public void SetHealth(float health, float maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
            if (healthSlider.value <= healthSlider.maxValue * 0.2f)
            {
                healthWarning.gameObject.SetActive(true);
            }
            else
            {
                healthWarning.gameObject.SetActive(false);
            }
        }

        public void SetMana(float mana, float maxMana)
        {
            manaSlider.maxValue = maxMana;
            manaSlider.value = mana;
            if(manaSlider.value <= manaSlider.maxValue * 0.2f)
            {
                manaWarning.gameObject.SetActive(true);
            }
            else
            {
                manaWarning.gameObject.SetActive(false);
            }
        }

        public UpgradePanel CreateUpgradePanel()
        {
            eventManager.OnTurnOnUI.RaiseEvent();
            if (GameObject.FindObjectOfType<UpgradePanel>() != null) return null;
            return Instantiate<UpgradePanel>(Resources.Load<UpgradePanel>("Prefabs/Game/Upgrade/UpgradePanel"), this.transform);
        }

        private void ShowStatsPressed()
        {
            isStatsShow = !isStatsShow;
            if (isStatsShow)
            {
                MoveIn(statsMenuRect);
                statsMenuUI.UpdateStats(supportUIComponent.OnPlayerStatsChangeEventArg);
            }
            else
            {
                MoveOut(statsMenuRect);
            }
        }

        private void UpdatePlayerSprite()
        {
            if(supportUIComponent.playerSprite != null)
                ChangePlayerSprite(supportUIComponent.playerSprite);
        }

        private void UpdateGoldText(object sender, int gold)
        {
            this.goldText.text = $"Gold: {gold}";
        }

        private void UpdateGunSprite(int number)
        {
            if(supportUIComponent.gun1Sprite == null || supportUIComponent.gun2Sprite == null) return;

            currentGunNumber = number;
            if(number == 1)
            {
                this.mainGun.sprite = supportUIComponent.gun1Sprite;
                this.subGun.sprite = supportUIComponent.gun2Sprite;
            }
            else
            {
                this.mainGun.sprite = supportUIComponent.gun2Sprite;
                this.subGun.sprite = supportUIComponent.gun1Sprite;
            }
        }

        private void UpdateGunSprite()
        {
           if(currentGunNumber == 1)
            {
                this.mainGun.sprite = supportUIComponent.gun1Sprite;
                this.subGun.sprite = supportUIComponent.gun2Sprite;
            }
            else
            {
                this.mainGun.sprite = supportUIComponent.gun2Sprite;
                this.subGun.sprite = supportUIComponent.gun1Sprite;
            } 
        }

        public virtual void MoveIn(RectTransform rect)
        {
            // Show
            Tween tweenContain = rect.DOLocalMoveX(-700, 0.3f).SetEase(Ease.OutExpo).Play();
            tweenContain.Play();
        }

        public void MoveOut(RectTransform rect)
        {
            // Hide
            statsMenuUI.UpdateStats(supportUIComponent.OnPlayerStatsChangeEventArg);
            float anchorPosX = -1300;
            Tween tweenContain = rect.DOLocalMoveX(anchorPosX, 0.05f).SetEase(Ease.InQuad);
            tweenContain.Play();
        }

        public void ChangePlayerSprite(Sprite playerSprite)
        {
            playerImage.sprite = playerSprite;
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnregisterToContainer()
        {
            SingletonContainer.UnRegister(this);
        }
    }
}
