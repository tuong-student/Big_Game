using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using Game.Save;

namespace Game.Player
{
    public class WeaponsHolder : MonoBehaviour
    {
        [SerializeField] GunScripts currentGun;
        [SerializeField] [Range(1, 9)] int gun1Index, gun2Index;

        [HideInInspector] public bool isShootPress;
        private float nextShootTime;

        private GunData gun1Data, gun2Data;
        private SaveModels.WeaponModel weaponModel;

        #region Bool
        bool isCheat = false;
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
        }

        private void Update()
        {
            //if (PlayerScripts.GetInstance == null) return;
            //if (PlayerScripts.GetInstance.isDead) return;
        }

        private void LateUpdate()
        {
            
        }

        private void OnDisable()
        {
            GameInput.OnMouseMove -= LookAtMouse;
            GameInput.OnPlayerShoot -= Fire;
        }

        private void Save()
        {
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

                gun1Data = WeaponManager.GetInstance.GetGunData(gun1Index);
                gun2Data = WeaponManager.GetInstance.GetGunData(gun2Index);
            }
        }

        GunData GetGunData(int index)
        {
                //WeaponManager.GetInstance.GetGunData(index);
            return null;
        }

        public void ChangeGun(int index)
        {
            switch (index)
            {
                case 1:
                    currentGun.SetData(gun1Data);
                    InGameUI.GetInstance.ChangeGunSprites(gun1Data.gunImage, gun2Data.gunImage);
                    break;
                case 2:
                    currentGun.SetData(gun2Data);
                    InGameUI.GetInstance.ChangeGunSprites(gun2Data.gunImage, gun1Data.gunImage);
                    break;
            }
            InGameUI.GetInstance.SetStats();
        }

        public bool SetNewGun(GunData data)
        {
            if (IsEnoughGun())
            {
                return false;
            }
            else
            {
                gun2Index = WeaponManager.GetInstance.GetIndexOf(data);
                gun2Data = data;
                InGameUI.GetInstance.ChangeGunSprites(gun1Data.gunImage, gun2Data.gunImage);
                return true;
            }
        }

        public bool IsEnoughGun()
        {
            if (gun2Index != gun1Index) return true;
            else return false;
        }

        public void ChangeNewGun(GunData data)
        {
            //Set gunIndex
            //if (this.currentGun.gunData.Equals(GetGunData(gun1Index)))
            //{
            //    gun1Index = WeaponManager.GetInstance.GetIndexOf(data);
            //    gun1Data = data;
            //    InGameUI.GetInstance.ChangeGunSprites(gun1Data.gunImage, gun2Data.gunImage);
            //}
            //else
            //{
            //    gun2Index = WeaponManager.GetInstance.GetIndexOf(data);
            //    gun2Data = data;
            //    InGameUI.GetInstance.ChangeGunSprites(gun2Data.gunImage, gun1Data.gunImage);
            //}

            ////Set new data
            //this.currentGun.SetData(data);
            //LocalDataManager.currentGun1Index = gun1Index;
            //LocalDataManager.currentGun2Index = gun2Index;
        }

        public GunData GetCurrentGunData()
        {
            return this.currentGun.gunData;
        }

        void Fire()
        {
            float fireRate = PlayerScripts.GetInstance.fireRate + GetCurrentGunData().fireRate;
            if(Time.time >= nextShootTime)
            {
                currentGun.Fire();
                nextShootTime = Time.time + 1 / fireRate;
            }
        }

        void LookAtMouse(Vector3 mousePos)
        {
            Vector3 mouseInWorldPos = NOOD.NoodyCustomCode.ScreenPointToWorldPoint(mousePos);
            NOOD.NoodyCustomCode.LookToPoint2D(this.transform, mouseInWorldPos);
        }
    }
}
