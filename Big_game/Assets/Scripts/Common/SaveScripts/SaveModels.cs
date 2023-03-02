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
        }

        public class PlayerModel
        {
            public float maxHealth;
            public float maxMana;
            public float criticalRate;
            public float fireRate;
            public float defence;
            public float walkSpeed;
            public float runSpeed;
            public float dashForce;
            public float dashTime;

            public int playerNum;
        }

        public class GameSystemModel
        {
            public int gold;
            public int level;
        }

        public class WeaponModel
        {
            public int gun1Index;
            public int gun2Index;
        }
    }
}
