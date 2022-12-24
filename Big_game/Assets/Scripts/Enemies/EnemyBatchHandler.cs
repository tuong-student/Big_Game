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
    private List<BaseEnemy> enemies = new List<BaseEnemy>();  
    [SerializeField]
    private GameObject batchDoor;
    public bool openDoor = false;

    private void Start() {
        foreach (Transform tr in GetComponentInChildren<Transform>()){
            if(tr != this)
                enemies.Add(tr.GetComponent<BaseEnemy>());
        }

        if(hasShooterEnemies)
        {
            foreach(Transform tr in shooterEnemyHolder.GetComponentInChildren<Transform>()){
                    shooterEnemies.Add(tr.GetComponent<EnemyShooterMovement>());
            }
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
        if(gameObject != null)
        enemies.Remove(enemy);
        CheckUnlockDoor();
    }
    public void RemoveShooterEnemy(EnemyShooterMovement shooterEnemy){
        if(shooterEnemies != null)
            shooterEnemies.Remove(shooterEnemy);
        CheckUnlockDoor();
    }
    void CheckUnlockDoor(){
        if(hasShooterEnemies)
        {
            if(enemies.Count == 0 && shooterEnemies.Count == 0)
            {
                if(batchDoor){
                    openDoor = true;
                }
            }
        }
        else{
            if(enemies.Count == 0){
                
                if(batchDoor){
                    openDoor = false;
                }
            }
        }
    }
   public bool SetOpenDoor(bool _openDoor){
        openDoor = _openDoor;
        return openDoor;
   } 
}

