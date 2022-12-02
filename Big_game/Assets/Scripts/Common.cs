using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{
  public static GameObject CreateGameObject(){
    GameObject testIsMoving = new GameObject();
    testIsMoving.AddComponent<BaseEnemy>();
    BaseEnemy BaseEnemyScript = testIsMoving.GetComponent<BaseEnemy>();

    testIsMoving.AddComponent<BoxCollider2D>();
    BoxCollider2D collider2D = testIsMoving.GetComponent<BoxCollider2D>();
    collider2D.offset = new Vector2(0.001622438f,0.007571734f);
    collider2D.size = new Vector2(0.06805706f,0.1448565f);

    // BaseEnemyScript.publicAwake();
    return testIsMoving;
  } 
}
