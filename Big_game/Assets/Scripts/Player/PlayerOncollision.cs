using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
[ExecuteInEditMode]
public class PlayerOncollision : MonoBehaviour
{
    [SerializeField] PlayerScripts playerScripts;
    [SerializeField] Collider2D collider;

    private void Awake()
    {
        PlayerScripts temp = GetComponent<PlayerScripts>();
        if (temp)
        {
            playerScripts = temp;
        }
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        EventManager.GetInstance.OnGenerateLevel.OnEventRaise += () => { collider.enabled = false; };
        EventManager.GetInstance.OnGenerateLevelComplete.OnEventRaise += () => { collider.enabled = true; };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerScripts.IsMoveable) return;
        if (collision.gameObject.CompareTag("Finish"))
        {
            LevelManager.GetInstance.OpenPortal();
        }

        if (collision.GetComponent<Portal>())
        {
            LevelManager.GetInstance.ClosePortal();
        }
    }
}
