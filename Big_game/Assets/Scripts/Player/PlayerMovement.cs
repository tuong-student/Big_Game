using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

namespace Game.Player
{
    [ExecuteInEditMode]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] PlayerScripts playerScripts;
        [HideInInspector] public Rigidbody2D myBody;
        [HideInInspector] public Vector3 movement;

        #region Bool
        bool isDashPress;
        bool isDashing;
        [HideInInspector] public bool isStop;
        #endregion

        private void Awake()
        {
            isDashPress = false;
            isDashing = false;
            PlayerScripts temp = GetComponent<PlayerScripts>();
            if (temp)
            {
                playerScripts = temp;
            }
            myBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            GameInput.OnPlayerMove += Move;
            GameInput.OnPlayerDash += DashPress;
        }

        private void Update()
        {
            if (!playerScripts.IsMoveable)
            {
                this.myBody.velocity = Vector3.zero;
                isStop = true;
                return;
            }
            if (playerScripts.isDead) return;
        }

        private void OnDisable()
        {
            GameInput.OnPlayerMove -= Move;
            GameInput.OnPlayerDash -= DashPress;
        }

        private void FixedUpdate()
        {
            if (isDashPress == true && isDashing == false) Dash();
        }

        private void Move(Vector2 movement)
        {
            if (!playerScripts.IsMoveable) return;
            this.movement = movement;
            this.myBody.velocity = movement * playerScripts.speed.value;
            if (movement == Vector2.zero)
            {
                if (Mathf.Abs(myBody.velocity.x) > 0.02f) myBody.drag = 2;
                isStop = true;
            }
            else
                isStop = false;
        }

        private void DashPress()
        {
            isDashPress = true;
        }

        private void Dash()
        {
            if (!playerScripts.IsMoveable) return;
            if (isDashing == false)
            {
                float manaAmount = 10f;
                if (movement == Vector3.zero) movement = new Vector3(1, 0);
                if(playerScripts.mana.value >= manaAmount) 
	            { 
                    myBody.AddForce(movement * playerScripts.dashForce, ForceMode2D.Impulse);
                    myBody.drag = 2;
                    isDashing = true;
                    playerScripts.MinusMana(manaAmount);
	            }

                StartCoroutine(ResetNormal());
                IEnumerator ResetNormal()
                {
                    yield return new WaitForSeconds(playerScripts.dashTime);
                    //return to normal speed
                    myBody.drag = 0;
                    isDashPress = false;
                    isDashing = false;
                }
            }
        }
    }
}