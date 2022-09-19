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
}
        