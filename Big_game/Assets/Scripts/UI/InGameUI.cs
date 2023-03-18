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
    public class InGameUI : MonoBehaviorInstance<InGameUI>
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider manaSlider;
        [SerializeField] private StatsMenuUI statsMenuUI;
        [SerializeField] private RectTransform statsMenuRect;
        [SerializeField] private Image mainGun, subGun, playerImage;
        [SerializeField] private GameCanvas gameCanvas;
        [SerializeField] private Text goldText;
        
        private bool isStatsShow = false;

        public float maxHealth = 100;

        public float maxMana = 50;
        public float currentMana;

        private void Awake()
        {
            gameCanvas = GetComponentInParent<GameCanvas>();
        }

        public void OnEnable()
        {
            this.AddTo(GameManager.GetInstance);

            EventManager.GetInstance.OnPlayerCreate += EventManager_OnPlayerCreate;

            GameManager.GetInstance.OnGoldChange += UpdateGoldText;

            GameInput.OnPlayerWatchStats += ShowStatsPressed;
            GameInput.OnPlayerChangeGun += UpdateGunSprite;
            WeaponsHolder.OnPickUpGun += WeaponsHolder_OnPickUpGun;

            SupportUIComponentHolder.GetInstance.OnUpdateHealth += SupportUIComponentHolder_OnUpdateHealth;
            SupportUIComponentHolder.GetInstance.OnUpdateMana += SupportUIComponentHolder_OnUpdateMana;

            NoodyCustomCode.StartDelayFunction(() =>
            {
                UpdatePlayerSprite();
                UpdateGunSprite(1);
            }, 0.2f);
        }

        public void Start()
        {
            SupportUIComponentHolder_OnUpdateHealth(null, SupportUIComponentHolder.GetInstance.OnPlayerHealthChangeEventArgs);
            SupportUIComponentHolder_OnUpdateMana(null, SupportUIComponentHolder.GetInstance.OnPlayerManaChangeEventArgs);
        }


        private void OnDisable()
        {

        }

        protected override void Dispose()
        {

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


        public void SetHealth(float health, float maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }

        public void SetMana(float mana, float maxMana)
        {
            manaSlider.maxValue = maxMana;
            manaSlider.value = mana;
        }

        public UpgradePanel CreateUpgradePanel()
        {
            EventManager.GetInstance.OnTurnOnUI.RaiseEvent();
            if (GameObject.FindObjectOfType<UpgradePanel>() != null) return null;
            return Instantiate<UpgradePanel>(Resources.Load<UpgradePanel>("Prefabs/Game/Upgrade/UpgradePanel"), this.transform);
        }

        private void ShowStatsPressed()
        {
            isStatsShow = !isStatsShow;
            if (isStatsShow)
            {
                MoveIn(statsMenuRect);
                statsMenuUI.ShowStats(Support.SupportUIComponentHolder.GetInstance.OnPlayerStatsChangeEventArg);
            }
            else
            {
                MoveOut(statsMenuRect);
            }
        }

        private void UpdatePlayerSprite()
        {
            ChangePlayerSprite(SupportUIComponentHolder.GetInstance.playerSprite);
        }

        private void UpdateGoldText(object sender, int gold)
        {
            this.goldText.text = $"Gold: {gold}";
        }

        private void UpdateGunSprite(int number)
        {
            if(number == 1)
            {
                this.mainGun.sprite = SupportUIComponentHolder.GetInstance.gun1Sprite;
                this.subGun.sprite = SupportUIComponentHolder.GetInstance.gun2Sprite;
            }
            else
            {
                this.mainGun.sprite = SupportUIComponentHolder.GetInstance.gun2Sprite;
                this.subGun.sprite = SupportUIComponentHolder.GetInstance.gun1Sprite;
            }
        }

        public virtual void MoveIn(RectTransform rect)
        {
            Tween tweenContain = rect.DOLocalMoveX(-700, 0.3f).SetEase(Ease.OutExpo).Play();
            tweenContain.Play();
        }

        public void MoveOut(RectTransform rect)
        {
            float anchorPosX = -1300;
            Tween tweenContain = rect.DOLocalMoveX(anchorPosX, 0.05f).SetEase(Ease.InQuad);
            tweenContain.Play();
        }

        public void ChangePlayerSprite(Sprite playerSprite)
        {
            playerImage.sprite = playerSprite;
        }
    }
}
