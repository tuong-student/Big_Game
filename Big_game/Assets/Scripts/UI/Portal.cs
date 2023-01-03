using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class Portal : AbstractMonoBehaviour
{
    [SerializeField] Animator portalAnim;
    [SerializeField] bool isMenuLevel;

    bool isOpen;

    private void Start()
    {
        if(isMenuLevel)
        {
            OpenAnimation();            
	    }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isOpen)
            {
                CloseAnimation();
                isOpen = false;
            }
            else
            {
                OpenAnimation();
                isOpen = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && isMenuLevel == false)
        {
            LevelManager.GetInstance.NextLevel();
        }
        if (collision.gameObject.tag.Equals("Player") && isMenuLevel)
        {
            EventManager.GetInstance.OnStartGame.OnEventRaise?.Invoke();
            EventManager.GetInstance.OnGenerateLevel.OnEventRaise?.Invoke();
        }
    }

    public void OpenAnimation()
    {
        portalAnim.SetTrigger("Open");
    }

    public void CloseAnimation()
    {
        portalAnim.SetTrigger("Close");
    }
}
