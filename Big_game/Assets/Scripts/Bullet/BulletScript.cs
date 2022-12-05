using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 1;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Blocking"))
        {
            GameObject explodePref = ExplodeManager.GetInstace.GetExplodePref(type);
            PoolingManager.GetInstace.SetExpldePoolingObject(explodePref);
            GameObject explode = PoolingManager.GetInstace.GetExplode();
            explode.transform.position = this.transform.position;
            explode.GetComponent<ParticleSystem>().Play();
            isBlock = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            if (collision.gameObject.GetComponent<BaseEnemy>())
            {
                BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
                enemy.TakeDamage(damage);
            }
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
