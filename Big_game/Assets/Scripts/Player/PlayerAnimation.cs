using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] PlayerScripts playerScripts;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sr;

    #region Bool
    [HideInInspector] public bool stand, up, down, left, right;
    #endregion

    private void Awake()
    {
        PlayerScripts temp = GetComponent<PlayerScripts>();
        if (temp)
        {
            playerScripts = temp;
        }

        anim = playerScripts.GetComponent<Animator>();
        sr = playerScripts.GetComponent<SpriteRenderer>();
        up = down = left = right = false;
        anim.SetBool("Stand", true);
    }

    private void Update()
    {
        anim.SetBool("Up", up);
        anim.SetBool("Down", down);
        anim.SetBool("Stand", stand);

        if (down)
        {
            anim.SetFloat("DownSpeedd", Mathf.Abs(playerScripts.playerMovement.myBody.velocity.y));
        }
        else
        {
            anim.SetFloat("DownSpeedd", 0);
        }

        if (left)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);

            anim.SetBool("Slide", true);
        }
        else if (right)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("Slide", true);
        }
        else
        {
            anim.SetBool("Slide", false);
        }

    }


}
