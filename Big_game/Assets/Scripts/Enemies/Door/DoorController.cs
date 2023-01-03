using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
  private BoxCollider2D myColider;
  private SpriteRenderer sr;
  [SerializeField]
  private EnemyBatchHandler enemyBatchHandler;
  private void Awake() {
    myColider = GetComponent<BoxCollider2D>();
    sr = GetComponent<SpriteRenderer>();
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
    if(other.gameObject.CompareTag("Player")&&enemyBatchHandler.openDoor ==true)
    {
      OpenDoor();
    }
  }
  private void OnTriggerExit2D(Collider2D other) {

    if(other.gameObject.CompareTag("Player")&&enemyBatchHandler.openDoor ==true){
        CloseDoor();
    }
  } 

}
