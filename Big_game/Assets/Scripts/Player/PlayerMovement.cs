using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerScripts playerScripts;
    Rigidbody2D myBody;
    Vector3 movement;

    #region Bool
    bool isDashPress;
    bool isDashing;
    #endregion

    private void Awake()
    {
<<<<<<< HEAD
=======
        isDashPress = false;
        isDashing = false;
        playerScripts.currentSpeed = playerScripts.runSpeed;
>>>>>>> updates/mainCharacter
        PlayerScripts temp = GetComponent<PlayerScripts>();
        if (temp)
        {
            playerScripts = temp;
        }
<<<<<<< HEAD
        playerScripts.currentSpeed = playerScripts.runSpeed;
=======
        myBody = GetComponent<Rigidbody2D>();
>>>>>>> updates/mainCharacter
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector3(moveX, moveY);

        if (Input.GetKeyDown(KeyCode.LeftShift)) isDashPress = true;

        Move(movement);
    }

    private void FixedUpdate()
    {
        if (isDashPress == true && isDashing == false) Dash();
    }

    private void Move(Vector3 movement)
    {
        this.transform.position += movement * playerScripts.currentSpeed * Time.deltaTime;
    }

    private void Dash()
    {
        if (movement == Vector3.zero) movement = new Vector3(1, 0);
        myBody.AddForce(movement * playerScripts.dashForce, ForceMode2D.Impulse);
        playerScripts.currentSpeed = 0;
        isDashing = true;
        myBody.drag = 2;

        StartCoroutine(ResetNormal());
        IEnumerator ResetNormal()
        {
            yield return new WaitForSeconds(playerScripts.dashTime);
            //return to normal speed
            myBody.velocity = Vector3.zero;
            myBody.drag = 0f;
            playerScripts.currentSpeed = playerScripts.runSpeed;
            isDashPress = false;
            isDashing = false;
        }
    }
}
        