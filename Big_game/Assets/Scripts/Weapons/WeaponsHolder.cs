using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHolder : MonoBehaviour
{
    [SerializeField] List<GunData> gunDatas = new List<GunData>();
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
        return gunDatas[index];
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
            gun2Index = gunDatas.IndexOf(data);
            return true;
        }
    }

    public bool IsEnoughGun()
    {
        if (gun2Index != gun1Index) return true;
        else return false;
    }

    public void SetCurrentGunData(GunData data)
    {
        if (this.currentGun.data.Equals(GetGunData(gun1Index)))
        {
            gun1Index = gunDatas.IndexOf(data);
        }
        else
        {
            gun2Index = gunDatas.IndexOf(data);
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
