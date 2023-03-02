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
            playerScripts.currentSpeed = playerScripts.runSpeed;
            myBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            GameInput.OnPlayerMove += Move;
            GameInput.OnPlayerDash += DashPress;
        }

        private void Update()
        {
         //   if (!playerScripts.IsMoveable)
         //   {
         //       this.myBody.velocity = Vector3.zero;
         //       isStop = true;
         //       return; 
	        //}
         //   if (playerScripts.isDead) return;

         //   float moveX = Input.GetAxisRaw("Horizontal");
         //   float moveY = Input.GetAxisRaw("Vertical");
         //   movement = new Vector3(moveX, moveY);


         //   if (Input.GetKeyDown(KeyCode.LeftShift)) isDashPress = true;

         //   Move(movement);

            
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
            this.myBody.velocity = movement * playerScripts.currentSpeed;
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
            if(isDashing == false)
            {
                float manaAmount = 10f;
                //playerScripts.dashEff.Play();
                if (movement == Vector3.zero) movement = new Vector3(1, 0);
                if(playerScripts.currentMana >= manaAmount) 
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
                    playerScripts.currentSpeed = playerScripts.runSpeed;
                    isDashPress = false;
                    isDashing = false;
                }
            }
        }
    }
}