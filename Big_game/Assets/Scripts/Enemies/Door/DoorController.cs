using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System.Enemy;

namespace Game.System
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private bool isFirstDoor;
        private BoxCollider2D myCollider;
        [SerializeField] private SpriteRenderer doorSr;
        [SerializeField] private SpriteRenderer outline;
        [SerializeField] private EnemyBatchHandler enemyBatchHandler;
        private bool isCanOpenDoor = false;

        private void Awake() {
            myCollider = GetComponent<BoxCollider2D>();
            if (isFirstDoor)
            {
                CanOpenDoor();
            }
            else
                enemyBatchHandler.OnIsOpenDoorTrue += CanOpenDoor;
        }

        public void OpenDoor(){
            myCollider.isTrigger = true;
            doorSr.enabled = false;
            outline.enabled = false;
            SingletonContainer.Resolve<AudioManager>().PlaySFX(sound.gateOpen);
        }

        public void CloseDoor(){
            myCollider.isTrigger = false;
            doorSr.enabled = true;
            outline.enabled = true;
        }

        private void CanOpenDoor()
        {
            outline.color = Color.green;
            isCanOpenDoor = true;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.CompareTag("Player") && isCanOpenDoor == true)
            {
                OpenDoor();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            CloseDoor();
        } 
    }
}
