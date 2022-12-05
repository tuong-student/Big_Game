using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScripts : MonoBehaviour
{
    [HideInInspector] public GunData gunData;
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
        this.gunData = data;
        sr.sprite = data.gunImage;
    }

    public void Fire()
    {
        PoolingManager.GetInstace.SetBulletPoolingObject(this.gunData.bulletPrefab);
        GameObject bullet = PoolingManager.GetInstace.GetBullet();
        bullet.transform.position = shootPoint.transform.position;
        bullet.transform.rotation = shootPoint.transform.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * gunData.bulletForce * 10f);
        SetBulletDamage(bullet, gunData.damage);

        anim.SetTrigger("Shooting");
        anim.SetInteger("AnimIndex", gunData.animationIndex);
    }


    void SetBulletDamage(GameObject bullet, float damage)
    {
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        // In the future damage will be randomed base on critical rate
        bulletScript.damage = damage;
    }
}
