using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatchHandler : MonoBehaviour
{
    [SerializeField]
    private List<BaseEnemy> enemies;  
    private void Start() {
        foreach (Transform tr in GetComponentInChildren<Transform>())
            if(tr != this)
                enemies.Add(tr.GetComponent<BaseEnemy>());
    }
    public void EnablePlayerTarget(){
        foreach (BaseEnemy enemy in enemies)
            enemy.HasPlayerTarget = true;
    }
    public void DisabledPlayerTarget(){
        foreach (BaseEnemy enemy in enemies)
            enemy.HasPlayerTarget = false;
    }
}

