using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Transform respawnPos;
    PlayerScripts player;

    private void Start()
    {
        player = PlayerScripts.Create(respawnPos);
        
        GameCanvas.Create();

        GameManager.Create();
        UIManager.Create();
        GoldManager.Create();
        PoolingManager.Create();
    }
}
