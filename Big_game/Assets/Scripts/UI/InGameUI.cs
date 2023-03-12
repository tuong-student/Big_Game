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
        [SerializeField] private Image gun1, gun2, playerImage;
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

        public void Start()
        {
        }

        public void OnEnable()
        {
            ChangePlayerImage();
            UpdateGunSprite(1);
            GameManager.GetInstance.OnGoldChange += UpdateGoldText;

            PlayerScripts.GetInstance.OnHealthChange += PlayerScripts_OnHealthChange;
            PlayerScripts.GetInstance.OnManaChange += PlayerScripts_OnManaChange;
            GameInput.OnPlayerWatchStats += ShowStatsPressed;
            GameInput.OnPlayerChangeGun += UpdateGunSprite;
            WeaponsHolder.OnPickUpGun += WeaponsHolder_OnPickUpGun;
        }

        private void OnDisable()
        {
            PlayerScripts.GetInstance.OnHealthChange -= PlayerScripts_OnHealthChange;
            PlayerScripts.GetInstance.OnManaChange -= PlayerScripts_OnManaChange;
            GameInput.OnPlayerWatchStats -= ShowStatsPressed;
            GameInput.OnPlayerChangeGun -= UpdateGunSprite;
            GameManager.GetInstance.OnGoldChange -= UpdateGoldText;
            WeaponsHolder.OnPickUpGun -= WeaponsHolder_OnPickUpGun;
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
                statsMenuUI.ShowStats(Support.SupportUIComponentHolder.GetInstance.onPlayerStatsChangeEventArg);
            }
            else
            {
                MoveOut(statsMenuRect);
            }
        }

        private void ChangePlayerImage()
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
                gun1.sprite = SupportUIComponentHolder.GetInstance.gun1Sprite;
                gun2.sprite = SupportUIComponentHolder.GetInstance.gun2Sprite;
            }
            else
            {
                gun1.sprite = SupportUIComponentHolder.GetInstance.gun2Sprite;
                gun2.sprite = SupportUIComponentHolder.GetInstance.gun1Sprite;
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
