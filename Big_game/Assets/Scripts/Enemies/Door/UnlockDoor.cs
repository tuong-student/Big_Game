using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    [SerializeField]
    private EnemyBatchHandler enemyBatchHandler;
    [SerializeField]
    private GameObject door;
   private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Door") && enemyBatchHandler.openDoor ==true)
        {
            door.SetActive(false);
        }
   }
   private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Door") && enemyBatchHandler.openDoor ==true)
        {
            door.SetActive(true);
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
        
    }
}
