using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] PlayerScripts playerScripts;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sr;
    Vector3 mouseDirection;

    #region Bool
    [HideInInspector] public bool stand, up, down, slide;
    #endregion

    private void Awake()
    {
        PlayerScripts temp = GetComponent<PlayerScripts>();
        if (temp)
        {
            playerScripts = temp;
        }
        up = down = slide = false;
        stand = true;
    }

    private void Update()
    {
        anim.SetBool("Up", up);
        anim.SetBool("Down", down);
        anim.SetBool("Stand", stand);
        anim.SetBool("Slide", slide);

        mouseDirection = NOOD.NoodyCustomCode.LookDirection(this.transform.position, NOOD.NoodyCustomCode.MouseToWorldPoint2D());

        if(mouseDirection == Vector3.zero)
        {
            stand = true;
            return;
        }

        if(mouseDirection.x == 0)
        {
            slide = false;
        }
        else
        {
            slide = true;
            if(mouseDirection.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }

        if(mouseDirection.y > 0)
        {
            up = true;
            down = false;
        }
        else if(mouseDirection.y < 0)
        {
            up = false;
            down = true;
        }
    }


}
