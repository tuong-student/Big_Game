using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public float damage = 1;
    [HideInInspector] public float backForce = 10f;
    public ExplodeType type;
    bool isBlock;

    private void OnEnable()
    {
        isBlock = false;
    }

    public void SetRange(float range)
    {
        GetComponent<SetObjectDeactive>().second = range;
    }

    public void SetBackForce(float backForce)
    {
        this.backForce = backForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Blocking"))
        {
            GameObject explodePref = ExplodeManager.GetInstace.GetExplodePref(type);
            PoolingManager.GetInstace.SetExpldePoolingObject(explodePref);
            GameObject explode = PoolingManager.GetInstace.GetExplode();
            explode.transform.position = this.transform.position;
            explode.GetComponent<ParticleSystem>().Play();
            if (collision.gameObject.GetComponent<BaseEnemy>())
            {
                BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
                enemy.TakeDamage(damage);
                enemy.GetComponent<Rigidbody2D>().AddForce(this.gameObject.transform.right * backForce);
            }
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (isBlock == true) return;
        GameObject explodePref = ExplodeManager.GetInstace.GetExplodePref(type);
        PoolingManager.GetInstace.SetExpldePoolingObject(explodePref);
        GameObject explode = PoolingManager.GetInstace.GetExplode();
        explode.transform.position = this.transform.position;
        explode.GetComponent<ParticleSystem>().Play();
    }
}
