using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Game.Player;
using UnityEngine.UI;

namespace Game.UI
{
    public class StatsMenuUI : MonoBehaviour
    {
        [SerializeField] private Text txtHealth, txtMana, txtDamage, txtCriticalRate, txtFireRate, txtCurrentSpeed, txtDefense;

        private void OnEnable()
        {
            Support.SupportUIComponentHolder.OnPlayerStatsUpdate += UpdateStats;
        }

        private void OnDisable()
        {
            Support.SupportUIComponentHolder.OnPlayerStatsUpdate -= UpdateStats;
        }

        private void EventManager_OnPlayerCreate(object sender, EventArgs eventArgs)
        {
            PlayerScripts player = sender as PlayerScripts;
        }

        public void UpdateStats(PlayerScripts.OnPlayerStatsChangeEventArg eventArg)
        {
            txtHealth.text = $"Health: {eventArg.maxHealth} (<color=#FF5353>{eventArg.bonusHealth}</color>)";
            txtMana.text = $"Mana: {eventArg.maxMana} (<color=#FF5353>{eventArg.bonusMana}</color>)";
            txtDamage.text = $"Damage: {eventArg.damage} (<color=#FF5353>{eventArg.bonusDamage}</color>)";
            txtCriticalRate.text = $"Critical: {eventArg.criticalRate} (<color=#FF5353>{eventArg.bonusCriticalRate}</color>)";
            txtFireRate.text = $"FireRate: {eventArg.fireRate} (<color=#FF5353>{eventArg.bonusFireRate}</color>)";
            txtCurrentSpeed.text = $"Speed: {eventArg.currentSpeed} (<color=#FF5353>{eventArg.bonusSpeed}</color>)";
            txtDefense.text = $"Defense: {eventArg.defense} (<color=#FF5353>{eventArg.bonusDefense}</color>)";
        }

    }
}
