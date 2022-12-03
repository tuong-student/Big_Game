using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHolder : MonoBehaviour
{
    [SerializeField] GunScripts currentGun;
    [SerializeField] [Range(0, 6)] int gun1Index, gun2Index;

    [HideInInspector] public bool isShootPress;
    private float nextShootTime;

    private void Start()
    {
        gun2Index = gun1Index;
        currentGun.SetData(GetGunData(gun1Index));
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
        return WeaponManager.GetInstace.GetGunData(index);
    }

    public void ChangeItem(int index)
    {
        switch (index)
        {
            case 1:
                currentGun.SetData(GetGunData(gun1Index));
                break;
            case 2:
                currentGun.SetData(GetGunData(gun2Index));
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
            gun2Index = WeaponManager.GetInstace.GetIndexOf(data);
            PlayerPrefs.Save();
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
            gun1Index = WeaponManager.GetInstace.GetIndexOf(data);
        }
        else
        {
            gun2Index = WeaponManager.GetInstace.GetIndexOf(data);
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
        currentGun.Fire();
        nextShootTime += 1 / currentGun.gunData.fireRate;
    }

    void LookAtMouse()
    {
        NOOD.NoodyCustomCode.LookToMouse2D(this.transform);
    }
}
