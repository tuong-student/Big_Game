using System.Security;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatchHandler : MonoBehaviour
{
    [SerializeField]
    private bool hasShooterEnemies;
    [SerializeField]
    private Transform shooterEnemyHolder;
    [SerializeField]
    private List<EnemyShooterMovement> shooterEnemies;

    [SerializeField]
    private List<BaseEnemy> enemies;  

    private void Start() {
        foreach (Transform tr in GetComponentInChildren<Transform>())
            if(tr != this)
                enemies.Add(tr.GetComponent<BaseEnemy>());

        if(hasShooterEnemies)
        {
            foreach(Transform tr in GetComponentInChildren<Transform>())
                if(tr != this)
                    shooterEnemies.Add(tr.GetComponent<EnemyShooterMovement>());
        }
    }
    public void EnablePlayerTarget(){
        foreach (BaseEnemy enemy in enemies)
            enemy.HasPlayerTarget = true;
    }
    public void DisabledPlayerTarget(){
        foreach (BaseEnemy enemy in enemies)
            enemy.HasPlayerTarget = false;
    }
    public void RemoveEnemy(BaseEnemy enemy){
        enemies.Remove(enemy);
        CheckUnlockgate();
    }
    public void RemoveShooterEnemy(EnemyShooterMovement shooterEnemy){
        if(shooterEnemies != null)
            shooterEnemies.Remove(shooterEnemy);
        CheckUnlockgate();
    }
    void CheckUnlockgate(){

    }
}

