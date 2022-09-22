using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerScripts playerScripts;

    private void Start()
    {
        PlayerScripts temp = GetComponent<PlayerScripts>();
        if (temp)
        {
            playerScripts = temp;
        }
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY);

        if (Input.GetKeyDown(KeyCode.LeftShift)) Dash();

        Move(movement);
    }

    private void Move(Vector3 movement)
    {
        this.transform.position += movement * playerScripts.currentSpeed * Time.deltaTime;
    }

    private void Dash()
    {
        playerScripts.currentSpeed = playerScripts.dashSpeed;

        StartCoroutine(ResetNormal());
        IEnumerator ResetNormal()
        {
            yield return new WaitForSeconds(playerScripts.dashTime);
            //return to normal speed
            playerScripts.currentSpeed = playerScripts.runSpeed;
        }
    }
}
        