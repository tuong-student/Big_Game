using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NOOD;
using Game.Player;

namespace Game.UI.Support
{
    public class SupportUIComponentHolder : AbstractMonoBehaviour, Game.Common.Interface.ISingleton
    {
        #region Events
        public static Action<PlayerScripts.OnPlayerStatsChangeEventArg> OnPlayerStatsUpdate;
        public EventHandler<PlayerScripts.OnHealthChangeEventArgs> OnUpdateHealth;
        public EventHandler<PlayerScripts.OnManaChangeEventArgs> OnUpdateMana;
        #endregion

        [HideInInspector] public Sprite playerSprite, gun1Sprite, gun2Sprite;
        [HideInInspector] public PlayerScripts.OnPlayerStatsChangeEventArg OnPlayerStatsChangeEventArg = new PlayerScripts.OnPlayerStatsChangeEventArg();
        [HideInInspector] public PlayerScripts.OnHealthChangeEventArgs OnPlayerHealthChangeEventArgs = new PlayerScripts.OnHealthChangeEventArgs();
        [HideInInspector] public PlayerScripts.OnManaChangeEventArgs OnPlayerManaChangeEventArgs = new PlayerScripts.OnManaChangeEventArgs();

        private PlayerScripts playerScripts;

        private void Awake()
        {
            RegisterToContainer();
            GameObject.FindObjectOfType<EventManager>().OnPlayerCreate += EventManager_OnPlayerCreate;

            OnPlayerHealthChangeEventArgs.maxHealth = 100f;
            OnPlayerHealthChangeEventArgs.health = 100f;

            OnPlayerManaChangeEventArgs.maxMana = 50f;
            OnPlayerManaChangeEventArgs.mana = 50f;
        }

        private void Start()
        {
            OnPlayerStatsChangeEventArg = new PlayerScripts.OnPlayerStatsChangeEventArg();
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {

        }

        protected override void Dispose()
        {
            if(playerScripts != null)
                playerScripts.OnPlayerStatsChange -= PlayerScript_UpdatePlayerStats;
        }

        private void EventManager_OnPlayerCreate(object sender, EventArgs eventArgs)
        {
            PlayerScripts player = sender as PlayerScripts;
            
            playerScripts = SingletonContainer.Resolve<PlayerScripts>();
            player.OnPlayerStatsChange += PlayerScript_UpdatePlayerStats;
            player.OnHealthChange += PlayerScript_OnHealthChange;
            player.OnManaChange += PlayerScript_OnManaChange;
            NoodyCustomCode.StartDelayFunction(player.ActiveOnPlayerStatsChange, 0.2f);
            this.AddTo(playerScripts);
        }

        private void PlayerScript_UpdatePlayerStats(object sender, PlayerScripts.OnPlayerStatsChangeEventArg eventArg)
        {
            Debug.Log("OnPlayerStatsChange");
            OnPlayerStatsChangeEventArg = eventArg;
            OnPlayerStatsUpdate?.Invoke(OnPlayerStatsChangeEventArg);
        }

        private void PlayerScript_OnHealthChange(object sender, PlayerScripts.OnHealthChangeEventArgs eventArgs)
        {
            OnPlayerHealthChangeEventArgs = eventArgs;
            OnUpdateHealth?.Invoke(this, OnPlayerHealthChangeEventArgs);
        }

        private void PlayerScript_OnManaChange(object sender, PlayerScripts.OnManaChangeEventArgs eventArgs)
        {
            OnPlayerManaChangeEventArgs = eventArgs;
            OnUpdateMana?.Invoke(this, OnPlayerManaChangeEventArgs);
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
