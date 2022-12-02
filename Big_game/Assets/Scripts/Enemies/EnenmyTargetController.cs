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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            if(enemyTargetType == EnemyTargetType.EnableEnemyTarget)
                enemyBatch.EnablePlayerTarget();
            else
                enemyBatch.DisabledPlayerTarget();
        }

    }
}
