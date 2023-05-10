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
        [SerializeField] private LayerMask blockingLayer;

        #region Bool
        bool isDashPress;
        bool isDashing;
        bool isBlock;
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

            if(IsBlock(movement) == false)
            {
                // Move normally
                ForceMove(movement);
            }
            else
            {
                // The direction is stop
                if(movement.x == 0 || movement.y == 0) return;     // Player only press 1 direction
                if(movement.x != 0 && IsBlock(new Vector2(movement.x, 0)) == false)
                {
                    // Can move in X axis
                    ForceMove(new Vector2(movement.x, 0).normalized);
                }
                if(movement.y != 0 && IsBlock(new Vector2(0, movement.y)) == false)
                {
                    // Can move in Y axis
                    ForceMove(new Vector2(0, movement.y).normalized);
                }
            }
        }

        private void ForceMove(Vector2 direction)
        {
            this.myBody.velocity = direction * playerScripts.speed.value;
            if (direction == Vector2.zero)
            {
                if (Mathf.Abs(myBody.velocity.x) > 0.02f) myBody.drag = 2;
                isStop = true;
            }
            else
                isStop = false;
        }

        private bool IsBlock(Vector2 direction)
        {
            if(Physics2D.Raycast(this.transform.position, direction, 0.2f, blockingLayer))
            {
                isBlock = true;
                return true;
            }
            else
            {
                isBlock = false;
                return false;
            }
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