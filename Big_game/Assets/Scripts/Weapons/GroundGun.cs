using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GroundGun : MonoBehaviour
{
    [SerializeField] GunData data;
    SpriteRenderer sr;
    PlayerScripts player;

    private void Start()
    {
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
