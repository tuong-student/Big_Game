using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GroundGun : MonoBehaviour
{
    GunData data;
    SpriteRenderer sr;
    PlayerScripts player;

    public static GroundGun Create(Transform parent = null)
    {
        return Instantiate<GroundGun>(Resources.Load<GroundGun>("Prefabs/Game/Weapons/groundGun"), parent);
    }

    private void Start()
    {
        data = WeaponManager.GetInstace.GetRandomGunData();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = data.gunImage;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerScripts>();
            player.SetGroundGun(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player.RemoveGroundGun();
        }
    }
}
