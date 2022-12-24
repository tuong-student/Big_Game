using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTargetType{
    EnableEnemyTarget,
    DisableEnemyTarget
}

public class EnenmyTargetController : MonoBehaviour
{
    [SerializeField]
    private EnemyTargetType enemyTargetType;
    [SerializeField]
    private EnemyBatchHandler enemyBatch;
    [SerializeField]
    private BossMovement boss;
    [SerializeField]
    private bool bossZone;

    private void OnTriggerEnter2D(Collider2D other) {
        if(bossZone){
            if(other.CompareTag("Player"))
            {
                if(enemyTargetType == EnemyTargetType.EnableEnemyTarget && boss)
                    boss.PlayerDetected(true);
                else if(enemyTargetType == EnemyTargetType.DisableEnemyTarget && boss) 
                    boss.PlayerDetected(false);
            }
        }else{
            if(other.CompareTag("Player"))
            {
                if(enemyTargetType == EnemyTargetType.EnableEnemyTarget)
                    enemyBatch.EnablePlayerTarget();
                else
                    enemyBatch.DisabledPlayerTarget();
            }
        }
    }
}
