using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    PlayerScripts player;

    private void Start()
    {
        player = PlayerScripts.Create();
    }
}
