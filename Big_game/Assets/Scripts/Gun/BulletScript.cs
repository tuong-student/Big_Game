using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 1;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable iDamageableObj = collision.gameObject.GetComponent<IDamageable>();
        if (iDamageableObj != null)
        {
            iDamageableObj.Damage(damage);
            Debug.Log(damage);
        }
        else
        {
            GameObject explode = PoolingManager.i.GetExplode();
            explode.transform.position = this.transform.position;
            explode.GetComponent<ParticleSystem>().Play();
        }
        
    }
}
