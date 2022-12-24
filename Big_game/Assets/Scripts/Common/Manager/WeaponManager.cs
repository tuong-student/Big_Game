using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class WeaponManager : MonoBehaviorInstance<WeaponManager>
{
    [SerializeField] List<GunData> gunDatas;
    public GunData shotgunData;

    public static WeaponManager Create(Transform parent = null)
    {
        return Instantiate<WeaponManager>(Resources.Load<WeaponManager>("Prefabs/Manager/WeaponManager"), parent);
    }

    public GunData GetGunData(int index)
    {
        return gunDatas[index];
    }

    public GunData GetRandomGunData()
    {
        return gunDatas[Random.Range(0, gunDatas.Count)];
    }

    public int GetIndexOf(GunData data)
    {
        return gunDatas.IndexOf(data);
    }
}
