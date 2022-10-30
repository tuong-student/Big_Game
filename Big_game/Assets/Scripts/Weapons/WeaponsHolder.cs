using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHolder : MonoBehaviour
{
    [SerializeField] GunScripts currentGun;
    [SerializeField] GunData gun1, gun2;

    [HideInInspector] public bool isShootPress;
    private float nextShootTime;

    private void Start()
    {
        currentGun.SetData(gun1);
    }

    private void Update()
    {
        LookAtMouse();
    }

    private void LateUpdate()
    {
        if (isShootPress && Time.time >= nextShootTime)
            Fire();
    }

    public void ChangeItem(int index)
    {
        switch (index)
        {
            case 1:
                currentGun.SetData(gun1);
                break;
            case 2:
                currentGun.SetData(gun2);
                break;
        }
    }

    void Fire()
    {
        nextShootTime = Time.time;
        currentGun.Fire();
        nextShootTime += 1 / currentGun.data.fireRate;
    }

    void LookAtMouse()
    {
        NOOD.NoodyCustomCode.LookToMouse2D(this.transform);
    }
}
