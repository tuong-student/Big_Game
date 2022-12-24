using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    private EnemyBatchHandler enemyBatchHandler;
    private void OnTriggerEnter(Collider other) {
        if(enemyBatchHandler.openDoor == true && other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        
        if(enemyBatchHandler.openDoor == true && other.CompareTag("Player"))
        {
            this.gameObject.SetActive(true);
        }
    }
}
