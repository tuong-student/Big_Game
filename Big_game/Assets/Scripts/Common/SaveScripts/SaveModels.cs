using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Save
{
    public class SaveModels
    {
        public enum SaveFile
        {
            PlayerSave,
            GameSystemSave,
            WeaponSave,
            UserSettingSave,
        }

        public class PlayerModel
        {
            public float maxHealth;
            public float maxMana;
            public float criticalRate;
            public float damage;
            public float fireRate;
            public float defense;
            public float speed;
            public float dashForce;
            public float dashTime;
        }

        public class GameSystemModel
        {
            public int gold;
            public int level;
            public int playerNum;
        }

        public class WeaponModel
        {
            public int gun1Index;
            public int gun2Index;
        }

        public class UserSetting
        {
            public float musicVolume;
            public float soundVolume;
        }
    }
}
