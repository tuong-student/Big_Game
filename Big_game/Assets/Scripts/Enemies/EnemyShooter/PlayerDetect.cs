using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    private EnemyShooterMovement enemyShooter;
    private void Awake() {
        enemyShooter = GetComponentInParent<EnemyShooterMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
            enemyShooter.SetPlayerInRange(true);    
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
            enemyShooter.SetPlayerInRange(false);
    }
}
