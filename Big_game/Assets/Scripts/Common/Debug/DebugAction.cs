using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD.NoodCamera;
using Game.Player;

namespace Game.DebugMenu
{
    public class DebugAction : MonoBehaviour
    {
        bool isCheat = false;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
         //   if (DebugUnclock.GetInstance.isDebug) 
	        //{ 
         //       if (Input.GetKeyDown(KeyCode.H))
         //       {
         //           PlayerScripts.GetInstance.Damage(30f);
         //       }
         //       if(Input.GetKeyDown(KeyCode.G))
         //       {
         //           PlayerScripts.GetInstance.AddHealth(30f);
	        //    }
         //       if(Input.GetKeyDown(KeyCode.M))
         //       {
         //           PlayerScripts.GetInstance.MinusMana(10f);
	        //    }
         //       if(Input.GetKeyDown(KeyCode.N))
         //       {
         //           PlayerScripts.GetInstance.AddMana(10f);
		       // }

         //       if (Input.GetKeyDown(KeyCode.Return) && !isCheat)
         //       {
         //           isCheat = true;
         //           EventManager.GetInstance.OnCheatEnable.RaiseEvent();
         //           PlayerScripts.GetInstance.isCheat = true;
         //           Debug.Log("Cheat Mode: " + isCheat);
         //           return;
	        //    }

         //       if(Input.GetKeyDown(KeyCode.Return) && isCheat)
         //       {
         //           isCheat = false;
         //           EventManager.GetInstance.OnCheatDisable.RaiseEvent();
         //           PlayerScripts.GetInstance.isCheat = false;
         //           Debug.Log("Cheat Mode: " + isCheat);
         //       }
         //   }
        }
    }
}
