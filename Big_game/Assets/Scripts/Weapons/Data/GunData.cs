using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "GunData", menuName = "ScriptableObjects/GunData")]
public class GunData : ScriptableObject
{
    public Sprite gunImage;
    public GameObject bulletPrefab;
    public int animationIndex;
    public float damage;
    public float fireRate;
    public float bulletForce = 1f;
}
