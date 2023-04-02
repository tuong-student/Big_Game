using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common.Manager;
using Game.System;

namespace Game.Player
{
    [ExecuteInEditMode]
    public class PlayerOncollision : MonoBehaviour
    {
        [SerializeField] PlayerScripts playerScripts;
        [SerializeField] Collider2D m_collider;
        bool isInteractable = false;

        private void Awake()
        {
            PlayerScripts temp = GetComponent<PlayerScripts>();
            if (temp)
            {
                playerScripts = temp;
            }
            m_collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
         //   EventManager.GetInstance.OnGenerateLevel.OnEventRaise += () => { if(m_collider) m_collider.enabled = false; };
         //   EventManager.GetInstance.OnGenerateLevelComplete.OnEventRaise += () => 
	        //{
         //       NOOD.NoodyCustomCode.StartDelayFunction(() => { if(m_collider) m_collider.enabled = true; }, 1.2f); 
	        //};
        }

        private void Update()
        {
         //   if(Input.GetKeyDown(KeyCode.E) && isInteractable)
         //   {
         //       isInteractPress = true;
	        //}
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!playerScripts.IsMoveable) return;
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                isInteractable = true;
                playerScripts.SetGroundObject(interactable);
            }

            if (collision.GetComponent<Portal>())
            {
                SingletonContainer.Resolve<LevelManager>().ClosePortal();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!playerScripts.IsMoveable) return;
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                playerScripts.SetGroundObject(interactable);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                isInteractable = false;
                playerScripts.SetGroundObject(null);
	        }
        }
    }
}
