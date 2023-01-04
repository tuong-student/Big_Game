using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHolder : MonoBehaviour
{
    [SerializeField] BaseEnemy boss;
    [SerializeField] Portal door;

    private void Update()
    {
        if(!boss.IsAlive())
        {
            door.Open();
        }
        else
        {
            door.Close();
	    }
    }
}
