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

        [SerializeField] private PlayerScripts playerScript;
        [SerializeField] private GunScripts currentGun;
        [SerializeField] [Range(1, 9)] private int gun1Index = 1, gun2Index = 1;

        [HideInInspector] public bool isShootPress;
        private float nextShootTime;

        private GunData gun1Data = null, gun2Data = null;
        private SaveModels.WeaponModel weaponModel;

        private WeaponManager weaponManager;
        #region Bool
        #endregion

        private void Awake()
        {
            LoadFromSave();

            nextShootTime = Time.time;
        }


        private void OnEnable()
        {
            GameInput.OnMouseMove += LookAtMouse;
            GameInput.OnPlayerShoot += Fire;
        }

        private void Start()
        {
            weaponManager = SingletonContainer.Resolve<WeaponManager>();
            playerScript = SingletonContainer.Resolve<PlayerScripts>();
            UpdateFromLoad();
            UpdateGunSprite();
            GameInput.OnPlayerChangeGun += ChangeGun;
            SingletonContainer.Resolve<EventManager>().OnGenerateLevel.OnEventRaise += Save;
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
            gun1Index = weaponManager.GetIndexOf(gun1Data);
            gun2Index = weaponManager.GetIndexOf(gun2Data);

            weaponModel.gun1Index = this.gun1Index;
            weaponModel.gun2Index = this.gun2Index;

        }

        private void LoadFromSave()
        {
            if(weaponModel == null)
            {
                weaponModel = new SaveModels.WeaponModel();
                weaponModel.gun1Index = this.gun1Index;
                weaponModel.gun2Index = this.gun2Index;
            }
            else
            {
                this.gun1Index = weaponModel.gun1Index;
                this.gun2Index = weaponModel.gun2Index;
            }
        }

        private void UpdateFromLoad()
        {
            gun1Data = weaponManager.GetGunData(gun1Index);
            gun2Data = weaponManager.GetGunData(gun2Index);

            currentGun.SetData(gun1Data);
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
            playerScript.ActiveOnPlayerStatsChange();
        }

        private void UpdateGunSprite()
        {
            SingletonContainer.Resolve<SupportUIComponentHolder>().gun1Sprite = gun1Data.gunIcon;
            SingletonContainer.Resolve<SupportUIComponentHolder>().gun2Sprite = gun2Data.gunIcon;
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
                // Pick up ground gun data and destroy its gameObject
                gun2Index = weaponManager.GetIndexOf(groundGun.GetData());
                gun2Data = groundGun.GetData();
                Destroy(groundGun.gameObject);
            }
            UpdateGunSprite();
            OnPickUpGun?.Invoke(this, EventArgs.Empty);
        }

        public bool IsEnoughGun()
        {
            if (gun2Data != null) return true;
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
            if (!playerScript.IsMoveable) return;
            float fireRate = playerScript.fireRate.value + GetCurrentGunData().fireRate;
            if(Time.time >= nextShootTime)
            {
                currentGun.Fire();
                nextShootTime = Time.time + 1 / fireRate;
                NOOD.NoodCamera.CameraShake.GetInstance.Shake();
            }
        }

        void LookAtMouse(Vector3 mousePos)
        {
            if (!playerScript.IsMoveable) return;
            Vector3 mouseInWorldPos = NOOD.NoodyCustomCode.ScreenPointToWorldPoint(mousePos);
            NOOD.NoodyCustomCode.LookToPoint2D(this.transform, mouseInWorldPos);
        }
    }
}
