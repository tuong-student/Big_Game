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
        #endregion

        [HideInInspector] public Sprite playerSprite, gun1Sprite, gun2Sprite;
        [HideInInspector] public PlayerScripts.OnPlayerStatsChangeEventArg onPlayerStatsChangeEventArg;

        private void Start()
        {
            PlayerScripts.GetInstance.OnPlayerStatsChange += UpdatePlayerStats;
        }

        private void UpdatePlayerStats(object sender, PlayerScripts.OnPlayerStatsChangeEventArg eventArg)
        {
            onPlayerStatsChangeEventArg = eventArg;
            OnPlayerStatsUpdate?.Invoke(onPlayerStatsChangeEventArg);
        }
    }
}
