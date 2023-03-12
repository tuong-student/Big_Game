using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.DebugMenu
{
    public enum DebugType
    {
        InfinityHealth,
        InfinityMana,
        MinusHealth30,
        MinusMana30,
        AddHealth30,
        AddMana30,
        AddGold30,
        OpenUpgradePanel,
        MaxFireRate,
        MaxDamage,
        MoveToBossRoom,
        SpawnGun,
        Save,
        Load
    }

    public class DebugMenu : MonoBehaviour
    {
        [SerializeField] private DebugAction debugAction;

        [SerializeField] private Button infinityHealth, infinityMana, minusHealth30, minusMana30, addHealth30, addMana30, addGold30, openUpgradPanel, maxFireRate, maxDamage, moveToBossRoom, spawnGun, save, load;

        [SerializeField] private Image i_infinityHealth, i_infinityMana, i_maxFireRate, i_maxDamage;

        private bool b_infinityHealth, b_infinityMana, b_maxFireRate, b_maxDamage;

        [SerializeField] private Color activeColor, inactiveColor;

        private void Start()
        {
            infinityHealth.onClick.AddListener(() => RequestAction(DebugType.InfinityHealth));
            infinityMana.onClick.AddListener(() => RequestAction(DebugType.InfinityMana));
            minusHealth30.onClick.AddListener(() => RequestAction(DebugType.MinusHealth30));
            minusMana30.onClick.AddListener(() => RequestAction(DebugType.MinusMana30));
            addHealth30.onClick.AddListener(() => RequestAction(DebugType.AddHealth30));
            addMana30.onClick.AddListener(() => RequestAction(DebugType.AddMana30));
            addGold30.onClick.AddListener(() => RequestAction(DebugType.AddGold30));
            openUpgradPanel.onClick.AddListener(() => RequestAction(DebugType.OpenUpgradePanel));
            maxFireRate.onClick.AddListener(() => RequestAction(DebugType.MaxFireRate));
            maxDamage.onClick.AddListener(() => RequestAction(DebugType.MaxDamage));
            moveToBossRoom.onClick.AddListener(() => RequestAction(DebugType.MoveToBossRoom));
            spawnGun.onClick.AddListener(() => RequestAction(DebugType.SpawnGun));
            save.onClick.AddListener(() => RequestAction(DebugType.Save));
            load.onClick.AddListener(() => RequestAction(DebugType.Load));
        }

        private void RequestAction(DebugType type)
        {
            debugAction.PerformAction(type);
            switch (type)
            {
                case DebugType.InfinityHealth:
                    b_infinityHealth = !b_infinityHealth;
                    SetButtonColor(i_infinityHealth, b_infinityHealth);
                    break;
                case DebugType.InfinityMana:
                    b_infinityMana = !b_infinityMana;
                    SetButtonColor(i_infinityMana, b_infinityMana);
                    break;
                case DebugType.MaxFireRate:
                    b_maxFireRate = !b_maxFireRate;
                    SetButtonColor(i_maxFireRate, b_maxFireRate);
                    break;
                case DebugType.MaxDamage:
                    b_maxDamage = !b_maxDamage;
                    SetButtonColor(i_maxDamage, b_maxDamage);
                    break;
            }
        }

        private void SetButtonColor(Image image, bool isActive)
        {
            if (isActive) image.color = activeColor;
            else image.color = inactiveColor;
        }
    }
}
