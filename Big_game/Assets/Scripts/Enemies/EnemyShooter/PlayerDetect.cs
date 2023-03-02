using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enemy;

namespace Game.System
{
    public class PlayerDetect : MonoBehaviour
    {
        private EnemyShooter enemyShooter;
        private void Awake() {
            enemyShooter = GetComponentInParent<EnemyShooter>();
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
}
