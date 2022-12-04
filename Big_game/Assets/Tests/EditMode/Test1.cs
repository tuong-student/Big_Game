using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test1
{
    // A Test behaves as an ordinary method

    [Test]
    public void Test1SimplePasses()
    {
        GameObject testIsMoving = new GameObject();
        testIsMoving.AddComponent<BaseEnemy>();
        BaseEnemy BaseEnemyScript = testIsMoving.GetComponent<BaseEnemy>();

        testIsMoving.AddComponent<BoxCollider2D>();
        BoxCollider2D collider2D = testIsMoving.GetComponent<BoxCollider2D>();
        collider2D.offset = new Vector2(0.001622438f,0.007571734f);
        collider2D.size = new Vector2(0.06805706f,0.1448565f);

        BaseEnemyScript.publicAwake();


        Vector3 input = new Vector3(1.0f,1.0f,0.0f);
        Vector3 expected = testIsMoving.GetComponent<Transform>().position + input;
        // Debug.LogFormat("Expected  {0}-{1}",expected.x,expected.y);
        BaseEnemyScript.HandleMovement(input.x,input.y);
        // Debug.LogFormat("Actual {0}-{1}",BaseEnemy.transform.position.x,BaseEnemy.transform.position.y);
        bool expectedCondition = testIsMoving.transform.position.x > 0f || testIsMoving.transform.position.y  > 0f;
        Assert.AreEqual(expectedCondition,true);
        // GameObject.Destroy(BaseEnemy);
    }
}
