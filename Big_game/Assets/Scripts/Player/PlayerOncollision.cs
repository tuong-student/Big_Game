using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
[ExecuteInEditMode]
public class PlayerOncollision : MonoBehaviour
{
    [SerializeField] PlayerScripts playerScripts;
    [SerializeField] Collider2D collider;
    bool isInteractPress = false;
    bool isInteractable = false;

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
        EventManager.GetInstance.OnGenerateLevel.OnEventRaise += () => { if(collider) collider.enabled = false; };
        EventManager.GetInstance.OnGenerateLevelComplete.OnEventRaise += () => 
	    {
            NOOD.NoodyCustomCode.StartDelayFunction(() => { if(collider) collider.enabled = true; }, 1.2f); 
	    };
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInteractable)
        {
            isInteractPress = true;
	    }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerScripts.IsMoveable) return;
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            isInteractable = true;
	    }
        if (collision.gameObject.CompareTag("Finish"))
        {
            LevelManager.GetInstance.OpenPortal();
        }

        if (collision.GetComponent<Portal>())
        {
            LevelManager.GetInstance.ClosePortal();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!playerScripts.IsMoveable) return;
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null && isInteractPress)
        {
            GroundGun temp = collision.gameObject.GetComponent<GroundGun>();
            if (temp != null)
            {
                playerScripts.PickUpGun(temp);
            }
            else
            {
                interactable.Interact();
            }
            isInteractPress = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null && isInteractable)
        {
            isInteractable = false;
	    }
    }
}
