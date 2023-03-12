using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI.Support;
using Game.Save;

namespace Game.Player.Weapon
{
    public class WeaponsHolder : MonoBehaviour
    {
        #region Event
        public static EventHandler OnPickUpGun;
        #endregion

        [SerializeField] private PlayerScripts playerSript;
        [SerializeField] private GunScripts currentGun;
        [SerializeField] [Range(1, 9)] private int gun1Index, gun2Index;

        [HideInInspector] public bool isShootPress;
        private float nextShootTime;

        private GunData gun1Data, gun2Data;
        private SaveModels.WeaponModel weaponModel;

        #region Bool
        #endregion

        private void Awake()
        {
            LoadFromSave();
            
            currentGun.SetData(gun1Data);
            nextShootTime = Time.time;
        }

        private void OnEnable()
        {
            GameInput.OnMouseMove += LookAtMouse;
            GameInput.OnPlayerShoot += Fire;
        }

        private void Start()
        {
            GameInput.OnPlayerChangeGun += ChangeGun;
            UpdateGunSprite();
        }

        private void Update()
        {

        }

        private void LateUpdate()
        {
            
        }

        private void OnDisable()
        {
            GameInput.OnMouseMove -= LookAtMouse;
            GameInput.OnPlayerShoot -= Fire;
            GameInput.OnPlayerChangeGun -= ChangeGun;
        }

        private void Save()
        {
            gun1Index = WeaponManager.GetInstance.GetIndexOf(gun1Data);
            gun2Index = WeaponManager.GetInstance.GetIndexOf(gun2Data);

            weaponModel.gun1Index = this.gun1Index;
            weaponModel.gun2Index = this.gun2Index;

            SaveJson.SaveToJson(weaponModel, SaveModels.SaveFile.WeaponSave.ToString());
        }

        private void LoadFromSave()
        {
            weaponModel = LoadJson<SaveModels.WeaponModel>.LoadFromJson(SaveModels.SaveFile.WeaponSave.ToString());
            if(weaponModel == null)
            {
                weaponModel = new SaveModels.WeaponModel();
                weaponModel.gun1Index = this.gun1Index;
                weaponModel.gun2Index = this.gun2Index;
                Save();
            }
            else
            {
                this.gun1Index = weaponModel.gun1Index;
                this.gun2Index = weaponModel.gun2Index;
            }

            gun1Data = WeaponManager.GetInstance.GetGunData(gun1Index);
            gun2Data = WeaponManager.GetInstance.GetGunData(gun2Index);
        }

        public void ChangeGun(int index)
        {
            switch (index)
            {
                case 1:
                    currentGun.SetData(gun1Data);
                    break;
                case 2:
                    currentGun.SetData(gun2Data);
                    break;
            }
            playerSript.ActiveOnPlayerStatsChange();
        }

        private void UpdateGunSprite()
        {
            SupportUIComponentHolder.GetInstance.gun1Sprite = gun1Data.gunIcon;
            SupportUIComponentHolder.GetInstance.gun2Sprite = gun2Data.gunIcon;
        }

        public void PickupNewGun(GroundGun groundGun)
        {
            if (IsEnoughGun())
            {
                // Swap ground gun data with current gun data
                GunData temp = GetCurrentGunData();
                if(gun1Data == GetCurrentGunData())
                {
                    gun1Data = groundGun.GetData();
                }
                else
                {
                    gun2Data = groundGun.GetData();
                }
                currentGun.SetData(groundGun.GetData());
                groundGun.SetData(temp);
            }
            else
            {
                // Pick up ground gun data and destroy its gameobject
                gun2Index = WeaponManager.GetInstance.GetIndexOf(groundGun.GetData());
                gun2Data = groundGun.GetData();
                Destroy(groundGun.gameObject);
            }
            UpdateGunSprite();
            OnPickUpGun?.Invoke(this, EventArgs.Empty);
        }

        public bool IsEnoughGun()
        {
            if (gun2Data != gun1Data) return true;
            else return false;
        }

        public GunData GetCurrentGunData()
        {
            return this.currentGun.gunData;
        }

        public void SetCurrentGunData(GunData gunData)
        {
            if (GetCurrentGunData() == gun1Data)
            {
                gun1Data = gunData;
            }
            else
            {
                gun2Data = gunData;
            }
        }

        void Fire()
        {
            if (!PlayerScripts.GetInstance.IsMoveable) return;
            float fireRate = PlayerScripts.GetInstance.fireRate.value + GetCurrentGunData().fireRate;
            if(Time.time >= nextShootTime)
            {
                currentGun.Fire();
                nextShootTime = Time.time + 1 / fireRate;
            }
        }

        void LookAtMouse(Vector3 mousePos)
        {
            if (!PlayerScripts.GetInstance.IsMoveable) return;
            Vector3 mouseInWorldPos = NOOD.NoodyCustomCode.ScreenPointToWorldPoint(mousePos);
            NOOD.NoodyCustomCode.LookToPoint2D(this.transform, mouseInWorldPos);
        }
    }
}
