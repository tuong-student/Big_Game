using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] PlayerScripts playerScripts;
        [SerializeField] Animator anim;
        [SerializeField] SpriteRenderer sr;
        [SerializeField] float fadeTime = 0.5f;
        Vector3 mouseDirection;

        #region Bool
        [HideInInspector] public bool stand, up, down, side;
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
            up = down = side = false;
            stand = true;
            playerMaterial = sr.material;
        }

        private void OnEnable()
        {
            GameInput.OnMouseMove += AnimationBaseOnMouse;
        }

        private void Start()
        {
            // true, false is that do you want to get component if that gameobject is not active.
            anim = GetComponentInChildren<Animator>(false);
            sr = GetComponentInChildren<SpriteRenderer>(false);
        }

        private void Update()
        {
            if (!playerScripts.IsMoveable) return;
            stand = playerScripts.playerMovement.isStop;
            anim.SetBool("Up", up);
            anim.SetBool("Down", down);
            anim.SetBool("Stand", stand);
            anim.SetBool("Side", side);
        }

        private void OnDisable()
        {
            GameInput.OnMouseMove -= AnimationBaseOnMouse;
        }

        public void GetAnimAndSrAgain()
        {
            anim = GetComponentInChildren<Animator>(false);
            sr = GetComponentInChildren<SpriteRenderer>(false);
        }

        public void AnimationBaseOnMouse(Vector3 mousePos)
        {
            if (!playerScripts.IsMoveable) return;
            mouseDirection = NOOD.NoodyCustomCode.LookDirection2D(this.transform.position, NOOD.NoodyCustomCode.ScreenPointToWorldPoint(mousePos));
            if (mouseDirection == Vector3.zero)
            {
                return;
            }

            if (mouseDirection.x == 0)
            {
                side = false;
            }
            else
            {
                side = true;
                if (mouseDirection.x < 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }
            }

            if (mouseDirection.y > 0.5f)
            {
                up = true;
                down = false;
            }
            else if (mouseDirection.y < -0.5f)
            {
                up = false;
                down = true;
            }
            else
            {
                up = false;
                down = false;
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
                if (fade <= 0)
                {
                    this.gameObject.SetActive(false);
                }

            }
        }

        public void Revive()
        {
            playerMaterial.SetFloat("_Fade", 1);
        }
    }
}
