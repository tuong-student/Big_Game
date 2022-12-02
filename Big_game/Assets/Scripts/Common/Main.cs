using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Main : MonoBehaviorInstance<Main>
{
    public Transform respawnPos;
    PlayerScripts player;


    private IEnumerator Start()
    {
        LevelManager.Create();
        GoldManager.Create();
        PoolingManager.Create();

        GameCanvas.Create().AddTo(this);
        GameManager.Create().AddTo(this);
        UIManager.Create().AddTo(this);

        Debug.Log(LocalDataManager.currentLevel);
        LevelManager.OnNextLevel += GenerateNewLevel;

        yield return null;
        if (respawnPos == null) respawnPos = GameObject.Find("RespawnPos").transform;
        player = (PlayerScripts) PlayerScripts.Create(respawnPos).AddTo(this);
    }

    public void GenerateNewLevel()
    {
        StartCoroutine(Co_GenerateNewLevel());
    }

    private IEnumerator Co_GenerateNewLevel()
    {
        Clear();
        GameCanvas.Create().AddTo(this);
        GameManager.Create().AddTo(this);
        UIManager.Create().AddTo(this);
        LevelManager.GetInstace.LoadLevel(LocalDataManager.currentLevel);
        yield return null;
        player = (PlayerScripts)PlayerScripts.Create(GameObject.Find("RespawnPos").transform).AddTo(this);
    }
}
