using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerOncollision : MonoBehaviour
{
    [SerializeField] PlayerScripts playerScripts;

    private void Awake()
    {
        PlayerScripts temp = GetComponent<PlayerScripts>();
        if (temp)
        {
            playerScripts = temp;
        }
    }
}
