using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 1;
    public ExplodeType type;


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
            GameObject explodePref = ExplodeManager.GetInstace.GetExplodePref(type);
            PoolingManager.GetInstace.SetExpldePoolingObject(explodePref);
            GameObject explode = PoolingManager.GetInstace.GetExplode();
            explode.transform.position = this.transform.position;
            explode.GetComponent<ParticleSystem>().Play();
        }
        
    }
}
