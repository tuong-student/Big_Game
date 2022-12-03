using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] PlayerScripts playerScripts;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float fadeTime = 0.5f;
    Vector3 mouseDirection;

    #region Bool
    [HideInInspector] public bool stand, up, down, slide;
    bool isDisappear = false;
    #endregion

    #region Private
    Material playerMaterial;
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
        playerMaterial = sr.material;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>(false);
        sr = GetComponentInChildren<SpriteRenderer>(false);
    }

    private void Update()
    {
        stand = playerScripts.playerMovement.isStop;
        anim.SetBool("Up", up);
        anim.SetBool("Down", down);
        anim.SetBool("Stand", stand);
        anim.SetBool("Slide", slide);

        mouseDirection = NOOD.NoodyCustomCode.LookDirection(this.transform.position, NOOD.NoodyCustomCode.MouseToWorldPoint2D());

        if(mouseDirection == Vector3.zero)
        {
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

    public void DeadAnimation()
    {
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        isDisappear = true;
        float fade = 1;
        while (fade > 0)
        {
            fade -= Time.deltaTime * fadeTime;
            playerMaterial.SetFloat("_Fade", fade);
            yield return null;
        }
    }
}
