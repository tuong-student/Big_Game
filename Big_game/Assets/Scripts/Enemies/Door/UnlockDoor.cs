using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    [SerializeField]
    private EnemyBatchHandler enemyBatchHandler;
    [SerializeField]
    private DoorController doorController;
   private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Door") && enemyBatchHandler.openDoor ==true)
        {
            // other.collider.enabled=false;
            doorController.OpenDoor();
        }
   }
   private void OnCollisionExit2D(Collision2D other) {
        Debug.Log("exit");
        if(other.gameObject.CompareTag("Door") && enemyBatchHandler.openDoor ==true)
        {

            // other.collider.enabled=true;
            doorController.CloseDoor();
        } 
    }
    private void OnTriggerExit2D(Collider2D other) {
        
        Debug.Log("exit");
        if(other.gameObject.CompareTag("Door") && enemyBatchHandler.openDoor ==true)
        {

            // other.collider.enabled=true;
            doorController.CloseDoor();
        } 
    }

}
