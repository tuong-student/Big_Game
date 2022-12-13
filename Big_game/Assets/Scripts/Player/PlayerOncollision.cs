using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerOncollision : MonoBehaviour
{
    [SerializeField] PlayerScripts playerScripts;

    private void Awake()
    {
        PlayerScripts temp = GetComponent<PlayerScripts>();
        if (temp)
        {
            playerScripts = temp;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            LevelManager.GetInstace.OpenPortal();
        }

        if (collision.gameObject.CompareTag("Portal"))
        {
            LevelManager.GetInstace.ClosePortal();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            LevelManager.GetInstace.OpenPortal();
        }

        if (collision.gameObject.CompareTag("Portal"))
        {
            LevelManager.GetInstace.ClosePortal();
        }
    }
}
