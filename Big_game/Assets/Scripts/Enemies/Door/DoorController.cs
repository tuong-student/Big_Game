using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System.Enemy;

namespace Game.System
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private bool isFirstDoor;
        private BoxCollider2D myColider;
        private SpriteRenderer sr;
        [SerializeField] private EnemyBatchHandler enemyBatchHandler;

        private void Awake() {
            myColider = GetComponent<BoxCollider2D>();
            sr = GetComponent<SpriteRenderer>();
            if (isFirstDoor)
                OpenDoor();
        }

        public void OpenDoor(){
            myColider.isTrigger = true;
            sr.enabled = false;
        }

        public void CloseDoor(){
            myColider.isTrigger = false;
            sr.enabled = true;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (isFirstDoor == true) return;
            if(other.gameObject.CompareTag("Player") && enemyBatchHandler.CheckUnlockDoor())
            {
                OpenDoor();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (isFirstDoor == true) return;
            CloseDoor();
        } 
    }
}
