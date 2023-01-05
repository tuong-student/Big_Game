using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;
using NOOD.NoodCamera;

public class DebugUnclock : MonoBehaviorInstance<DebugUnclock>
{
    public bool isDebug = false;
    int debugCount, maxDebugCount = 4;

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            if(Input.GetKeyDown(KeyCode.T))
            { 
                debugCount++;
                Debug.Log("DebugCount: " + debugCount);
	        }
            else
            {
                debugCount = 0;
	        }
	    }

        if (debugCount >= maxDebugCount) 
	    {
            isDebug = !isDebug;
            CameraShake.GetInstance.Shake();
            debugCount = 0;
            Debug.Log("Debug Mode: " + isDebug);
	    }
    }
}
