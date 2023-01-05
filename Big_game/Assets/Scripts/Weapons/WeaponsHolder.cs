using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHolder : MonoBehaviour
{
    [SerializeField] GunScripts currentGun;
    [SerializeField] [Range(1, 9)] int gun1Index, gun2Index;

    private GunData gun1Data, gun2Data;

    [HideInInspector] public bool isShootPress;
    private float nextShootTime;

    #region Bool
    bool isCheat = false;
    #endregion

    private void Start()
    {
        if(gun2Index == 0)
        {
            gun2Index = gun1Index;
	    }
        gun1Index -= 1;
        gun2Index -= 1;
        gun1Data = GetGunData(gun1Index);
        gun2Data = GetGunData(gun2Index);
        currentGun.SetData(GetGunData(gun1Index)); 
	    InGameUI.GetInstance.ChangeGunSprites(gun1Data.gunImage, gun2Data.gunImage);

        EventManager.GetInstance.OnCheatEnable.OnEventRaise += () => { isCheat = true; };
        EventManager.GetInstance.OnCheatDisable.OnEventRaise += () => { isCheat = false; };
    }

    private void Update()
    {
        if (PlayerScripts.GetInstance.isDead) return;
        LookAtMouse();
    }

    private void LateUpdate()
    {
        if (isShootPress && Time.time >= nextShootTime)
            Fire();
    }

    GunData GetGunData(int index)
    {
        return WeaponManager.GetInstance.GetGunData(index);
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
        if (this.currentGun.gunData.Equals(GetGunData(gun1Index)))
        {
            gun1Index = WeaponManager.GetInstance.GetIndexOf(data);
            gun1Data = data;
            InGameUI.GetInstance.ChangeGunSprites(gun1Data.gunImage, gun2Data.gunImage);
        }
        else
        {
            gun2Index = WeaponManager.GetInstance.GetIndexOf(data);
            gun2Data = data;
            InGameUI.GetInstance.ChangeGunSprites(gun2Data.gunImage, gun1Data.gunImage);
        }

        //Set new data
        this.currentGun.SetData(data);

    }

    public GunData GetCurrentGunData()
    {
        return this.currentGun.gunData;
    }

    void Fire()
    {
        nextShootTime = Time.time;
        if (currentGun.gunData == WeaponManager.GetInstance.shotgunData)
            currentGun.Fire(true);
        else currentGun.Fire(false);

        if(isCheat)
            nextShootTime += 1 / 3;
        else       
	        nextShootTime += 1 / currentGun.gunData.fireRate;
    }

    void LookAtMouse()
    {
        NOOD.NoodyCustomCode.LookToMouse2D(this.transform);
    }
}
