using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHolder : MonoBehaviour
{
    [SerializeField] GunScripts currentGun;
    [SerializeField] GunData gun1, gun2;

    [HideInInspector] public bool isShootPress;
    private float nextShootTime;

    private void Start()
    {
        currentGun.SetData(gun1);
    }

    private void Update()
    {
        LookAtMouse();
    }

    private void LateUpdate()
    {
        if (isShootPress && Time.time >= nextShootTime)
            Fire();
    }

    public void ChangeItem(int index)
    {
        switch (index)
        {
            case 1:
                currentGun.SetData(gun1);
                break;
            case 2:
                currentGun.SetData(gun2);
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
            gun2 = data;
            return true;
        }
    }

    public bool IsEnoughGun()
    {
        if (gun2 != null) return true;
        else return false;
    }

    public void SetCurrentGunData(GunData data)
    {
        if (this.currentGun.data.Equals(gun1))
        {
            gun1 = data;
        }
        else
        {
            gun2 = data;
        }
        this.currentGun.SetData(data);
    }

    public GunData GetCurrentGunData()
    {
        return this.currentGun.data;
    }

    void Fire()
    {
        nextShootTime = Time.time;
        currentGun.Fire();
        nextShootTime += 1 / currentGun.data.fireRate;
    }

    void LookAtMouse()
    {
        NOOD.NoodyCustomCode.LookToMouse2D(this.transform);
    }
}
