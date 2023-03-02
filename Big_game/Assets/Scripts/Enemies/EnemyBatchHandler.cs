using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using Game.Enemy;

namespace Game.System.Enemy
{
    public class EnemyBatchHandler : MonoBehaviour
    {
        private readonly string PLAYER_TAG = "Player";

        private EnemyShooter[] shooterEnemies;
        private BaseEnemy[] enemies;  

        [SerializeField] private bool hasShooterEnemies;

        public bool openDoor = false;

        private void Start(){ 
            enemies = GetComponentsInChildren<BaseEnemy>();

            shooterEnemies = GetComponentsInChildren<EnemyShooter>();

            if (shooterEnemies.Length > 0) hasShooterEnemies = true;
            else hasShooterEnemies = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(PLAYER_TAG))
            {
                EnablePlayerTarget();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(PLAYER_TAG))
            {
                DisabledPlayerTarget();
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

        public void CheckUnlockDoor(){
            if (this.transform.childCount <= 0) openDoor = true;
        }

        public void SetOpenDoor(bool _openDoor){
            openDoor = _openDoor;
        }
    }
}
