using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;
using NOOD.NoodCamera;

namespace Game.DebugMenu
{
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
	            }
                else
                {
                    debugCount = 0;
	            }
	        }

            if (debugCount >= maxDebugCount) 
	        {
                debugCount = 0;
                isDebug = true;
                CameraShake.GetInstance.Shake();
                EventManager.GetInstance.OnDebugEnable.RaiseEvent();
                
                Debug.Log("Debug Mode: " + isDebug);
	        }
        }

        public void CloseDebugMenu()
        {
            isDebug = false;
            EventManager.GetInstance.OnDebugDisable.RaiseEvent();
        }
    }
}
