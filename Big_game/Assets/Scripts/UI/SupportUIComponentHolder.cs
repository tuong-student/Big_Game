using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NOOD;
using Game.Player;

namespace Game.UI.Support
{
    public class SupportUIComponentHolder : MonoBehaviorInstance<SupportUIComponentHolder>
    {
        #region Events
        public static Action<PlayerScripts.OnPlayerStatsChangeEventArg> OnPlayerStatsUpdate;
        public EventHandler<PlayerScripts.OnHealthChangeEventArgs> OnUpdateHealth;
        public EventHandler<PlayerScripts.OnManaChangeEventArgs> OnUpdateMana;
        #endregion

        [HideInInspector] public Sprite playerSprite, gun1Sprite, gun2Sprite;
        [HideInInspector] public PlayerScripts.OnPlayerStatsChangeEventArg OnPlayerStatsChangeEventArg;
        [HideInInspector] public PlayerScripts.OnHealthChangeEventArgs OnPlayerHealthChangeEventArgs;
        [HideInInspector] public PlayerScripts.OnManaChangeEventArgs OnPlayerManaChangeEventArgs;

        private void Awake()
        {
            EventManager.GetInstance.OnPlayerCreate += EventManager_OnPlayerCreate;
            
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
            PlayerScripts.GetInstance.OnPlayerStatsChange -= PlayerScript_UpdatePlayerStats;
        }

        private void EventManager_OnPlayerCreate(object sender, EventArgs eventArgs)
        {
            PlayerScripts player = sender as PlayerScripts;

            player.OnPlayerStatsChange += PlayerScript_UpdatePlayerStats;
            player.OnHealthChange += PlayerScript_OnHealthChange;
            player.OnManaChange += PlayerScript_OnManaChange;
            this.AddTo(PlayerScripts.GetInstance);
        }

        private void PlayerScript_UpdatePlayerStats(object sender, PlayerScripts.OnPlayerStatsChangeEventArg eventArg)
        {
            OnPlayerStatsChangeEventArg = eventArg;
            OnPlayerStatsUpdate?.Invoke(OnPlayerStatsChangeEventArg);
        }

        private void PlayerScript_OnHealthChange(object sender, PlayerScripts.OnHealthChangeEventArgs eventArgs)
        {
            OnPlayerHealthChangeEventArgs = eventArgs;
        }

        private void PlayerScript_OnManaChange(object sender, PlayerScripts.OnManaChangeEventArgs eventArgs)
        {
            OnPlayerManaChangeEventArgs = eventArgs;
        }
    }
}
