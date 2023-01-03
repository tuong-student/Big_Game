using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GroundGun : MonoBehaviour, IInteractable
{
    [SerializeField] GunData data;
    SpriteRenderer sr;
    PlayerScripts player;

    public static GroundGun Create(Transform parent = null)
    {
        return Instantiate<GroundGun>(Resources.Load<GroundGun>("Prefabs/Game/Weapons/groundGun"), parent);
    }

    private void Start()
    {
        if(data == null)
            data = WeaponManager.GetInstance.GetRandomGunData();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = data.gunImage;
        EventManager.GetInstance.OnGenerateLevel.OnEventRaise += DestroyThis;
    }

    private void OnDisable()
    {
        EventManager.GetInstance.OnGenerateLevel.OnEventRaise -= DestroyThis;
    }

    public void DestroyThis()
    {
        if (this) Destroy(this.gameObject);
    }

    public void SetData(GunData data)
    {
        this.data = data;
        sr.sprite = data.gunImage;
    }

    public GunData GetData()
    {
        return this.data;
    }

    public void Interact()
    {
        
    }
}
