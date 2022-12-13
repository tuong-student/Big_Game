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

    public void Fire(bool isShotgun)
    {
        if (isShotgun)
        {
            PoolingManager.GetInstace.SetBulletPoolingObject(this.gunData.bulletPrefab);
            GameObject[] bullets = new GameObject[3];
            bullets[0] = PoolingManager.GetInstace.GetBullet();
            bullets[1] = PoolingManager.GetInstace.GetBullet();
            bullets[2] = PoolingManager.GetInstace.GetBullet();
            foreach(var b in bullets)
            {
                b.transform.position = shootPoint.transform.position;
                SetBulletDamage(b, gunData.damage);
            }

            bullets[0].GetComponent<Rigidbody2D>().AddForce((new Vector3(-0.5f, -0.5f) + shootPoint.right).normalized * gunData.bulletForce * 10f);
            bullets[1].GetComponent<Rigidbody2D>().AddForce(shootPoint.right * gunData.bulletForce * 10f);
            bullets[2].GetComponent<Rigidbody2D>().AddForce((new Vector3(0.5f, 0.5f) + shootPoint.right).normalized * gunData.bulletForce * 10f);
            
        }
        else
        {
            PoolingManager.GetInstace.SetBulletPoolingObject(this.gunData.bulletPrefab);
            GameObject bullet = PoolingManager.GetInstace.GetBullet();
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.SetRange(gunData.range);
            bullet.transform.position = shootPoint.transform.position;
            bullet.transform.rotation = shootPoint.transform.rotation;
            bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * gunData.bulletForce * 10f);
            SetBulletDamage(bullet, gunData.damage);
        }

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
