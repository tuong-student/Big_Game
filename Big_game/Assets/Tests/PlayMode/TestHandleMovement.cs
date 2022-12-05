using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class NewTestScript
{
    
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {

        GameObject go = Common.CreateGameObject();
        BaseEnemy script = go.GetComponent<BaseEnemy>();
        script.publicAwake();
        Vector3 input = new Vector3(1.0f,1.0f,0.0f);
        Vector3 expected = go.GetComponent<Transform>().position + 2*input;
        Debug.LogFormat("Expected  {0}-{1}",expected.x,expected.y);
        script.HandleMovement(input.x,input.y);
        yield return new WaitForSeconds(2);

        Debug.LogFormat("Actual {0}-{1}",go.transform.position.x,go.transform.position.y);
        Vector3 vector = go.GetComponent<Transform>().position - expected;
        Assert.AreEqual(vector.magnitude < 0.01f , true);

    }
}
