using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScripts : MonoBehaviour
{
    public GunData data;
    public SpriteRenderer sr;
    public Animator anim;
    [SerializeField] Transform shootPoint;

    #region
    ObjectPool bulletPooling;
    #endregion

    private void Awake()
    {
        if(sr == null)
            sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
    }

    public void SetData(GunData data)
    {
        this.data = data;
        PoolingManager.i.SetBulletPoolingObject(data.bulletPrefab);
        sr.sprite = data.gunImage;
    }

    public void Fire()
    {
        GameObject bullet = PoolingManager.i.GetBullet();
        bullet.transform.position = shootPoint.transform.position;
        bullet.transform.rotation = shootPoint.transform.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * data.bulletForce * 10f);
        SetBulletDamage(bullet, data.damage);

        anim.SetTrigger("Shooting");
        anim.SetInteger("AnimIndex", data.animationIndex);
    }


    void SetBulletDamage(GameObject bullet, float damage)
    {
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        // In the future damage will be randomed base on critical rate
        bulletScript.damage = damage;
    }
}
