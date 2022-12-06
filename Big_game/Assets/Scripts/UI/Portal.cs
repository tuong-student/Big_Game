using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class Portal : AbstractMonoBehaviour
{
    [SerializeField] Animator potalAnim;
    bool isOpen;

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
        if (collision.gameObject.tag.Equals("Player"))
        {
            LevelManager.GetInstace.NextLevel();
        }
    }

    public void OpenAnimation()
    {
        potalAnim.SetTrigger("Open");
    }

    public void CloseAnimation()
    {
        potalAnim.SetTrigger("Close");
    }
}
