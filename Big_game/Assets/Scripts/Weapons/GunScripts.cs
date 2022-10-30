using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScripts : ObjectPool
{
    public GunData data;
    public SpriteRenderer sr;
    public Animator anim;
    [SerializeField] Transform shootPoint;
    [SerializeField] ObjectPool ExplodeSpawn;

    private void Awake()
    {
        if(sr == null)
            sr = GetComponent<SpriteRenderer>();
    }

    public void SetData(GunData data)
    {
        this.data = data;
        objectToPool = data.bulletPrefab;
        sr.sprite = data.gunImage;
    }

    public void Fire()
    {
        GameObject bullet = GetPoolObject();
        bullet.transform.rotation = this.transform.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * data.bulletForce * 10f);
        SetObjectDeactive deactiveBullet = bullet.GetComponent<SetObjectDeactive>();
        deactiveBullet.action = () =>
        {
            GameObject explode = ExplodeSpawn.GetPoolObject();
            explode.transform.position = deactiveBullet.transform.position;
            explode.GetComponent<ParticleSystem>().Play();
        };

        anim.SetTrigger("Shooting");
        anim.SetInteger("AnimIndex", data.animIndex);
    }
}
