using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class Portal : AbstractMonoBehaviour
{
    [SerializeField] Animator portalAnim;
    [SerializeField] bool isMenuLevel;
    [SerializeField] bool isLastLevel;
    [SerializeField] Collider2D myCollider;

    bool isOpen;

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
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isOpen)
            {
                Close();
                isOpen = false;
            }
            else
            {
                Open();
                isOpen = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        { 
            if(isMenuLevel)
            {
                EventManager.GetInstance.OnStartGame.RaiseEvent();
                EventManager.GetInstance.OnGenerateLevel.RaiseEvent();
            }
            else if(isLastLevel)
            {
                EventManager.GetInstance.OnWinGame.RaiseEvent();
	        }
            else
            {
                LevelManager.GetInstance.NextLevel();
                EventManager.GetInstance.OnGenerateLevel.RaiseEvent();
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
