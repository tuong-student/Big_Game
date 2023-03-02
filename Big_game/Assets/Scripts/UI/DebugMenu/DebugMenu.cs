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
        MaxFireRate,
        MaxDamage,
        MoveToBossRoom,
        SpawnGun
    }

    public class DebugMenu : MonoBehaviour
    {
        [SerializeField] private DebugAction debugAction;

        [SerializeField] private Button infinityHealth, infinityMana, minusHealth30, minusMana30, addHealth30, addMana30, maxFireRate, maxDamage, moveToBossRoom, spawnGun;

        [SerializeField] private Image i_infinityHealth, i_infinityMana, i_minusHealth30, i_minusMana30, i_addHealth30, i_addMana30, i_maxFireRate, i_maxDamage, i_moveToBossRoom, i_spawnGun;

        private bool b_infinityHealth, b_infinityMana, b_minusHealth30, b_minusMana30, b_addHealth30, b_addMana30, b_maxFireRate, b_maxDamage;

        [SerializeField] private Color activeColor, inactiveColor;

        private void Start()
        {
            infinityHealth.onClick.AddListener(() => RequestAction(DebugType.InfinityHealth));
            infinityMana.onClick.AddListener(() => RequestAction(DebugType.InfinityMana));
            minusHealth30.onClick.AddListener(() => RequestAction(DebugType.MinusHealth30));
            minusMana30.onClick.AddListener(() => RequestAction(DebugType.MinusMana30));
            addHealth30.onClick.AddListener(() => RequestAction(DebugType.AddHealth30));
            addMana30.onClick.AddListener(() => RequestAction(DebugType.AddMana30));
            maxFireRate.onClick.AddListener(() => RequestAction(DebugType.MaxFireRate));
            maxDamage.onClick.AddListener(() => RequestAction(DebugType.MaxDamage));
            moveToBossRoom.onClick.AddListener(() => RequestAction(DebugType.MoveToBossRoom));
            spawnGun.onClick.AddListener(() => RequestAction(DebugType.SpawnGun));
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
