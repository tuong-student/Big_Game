using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;
using NOOD.NoodCamera;

namespace Game.DebugMenu
{
    public class DebugUnlock : MonoBehaviour, Game.Common.Interface.ISingleton
    {
        public bool isDebug = false;
        int debugCount, maxDebugCount = 4;

        private EventManager eventManager;

        void Awake()
        {
            RegisterToContainer();
        }

        void Start()
        {
            eventManager = SingletonContainer.Resolve<EventManager>();
        }

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
                eventManager.OnDebugEnable.RaiseEvent();
                
                Debug.Log("Debug Mode: " + isDebug);
	        }
        }

        public void CloseDebugMenu()
        {
            isDebug = false;
            eventManager.OnDebugDisable.RaiseEvent();
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnregisterToContainer()
        {
            SingletonContainer.Register(this);
        }
    }
}
