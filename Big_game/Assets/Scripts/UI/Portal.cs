using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;
using NOOD;

namespace Game.System
{
    public class Portal : AbstractMonoBehaviour
    {
        [SerializeField] private Animator portalAnim;
        [SerializeField] private bool isMenuLevel;
        [SerializeField] private bool isLastLevel;
        [SerializeField] private Collider2D myCollider;

        private bool isOpen;

        private void Start()
        {
            myCollider = GetComponent<Collider2D>();
            if(isMenuLevel)
            {
                Open();            
	        }
        }

        private void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            { 
                if(isMenuLevel)
                {
                    EventManager.GetInstance.OnStartGame.RaiseEvent();
                }
                else if(isLastLevel)
                {
                    EventManager.GetInstance.OnWinGame.RaiseEvent();
	            }
                else
                {
                    LevelManager.GetInstance.NextLevel();
                }
	        }
        }

        public void Open()
        {
            myCollider.isTrigger = true;
            portalAnim.SetTrigger("Open");
        }

        public void Close()
        {
            myCollider.isTrigger = false;
            portalAnim.SetTrigger("Close");
        }
    }
}
