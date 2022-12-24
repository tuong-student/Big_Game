using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
  private BoxCollider2D myColider;
  private SpriteRenderer sr;
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
}
