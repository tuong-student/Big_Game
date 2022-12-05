using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Main : MonoBehaviorInstance<Main>
{
    Transform respawnPos;
    PlayerScripts player;


    private IEnumerator Start()
    {
        LocalDataManager.Load();

        LevelManager.Create();
        GoldManager.Create();
        PoolingManager.Create();
        ExplodeManager.Create();
        WeaponManager.Create();
        GameManager.Create();

        GameCanvas.Create().AddTo(this);
        UIManager.Create().AddTo(this);

        Debug.Log(LocalDataManager.currentLevel);
        LevelManager.OnNextLevel += GenerateNewLevel;

        yield return new WaitForSeconds(1f);
        if (respawnPos == null) respawnPos = GameObject.Find("RespawnPos").transform;
        player = (PlayerScripts) PlayerScripts.Create(respawnPos).AddTo(this);
    }

    public void GenerateNewLevel()
    {
        StartCoroutine(Co_GenerateNewLevel());
    }

    private IEnumerator Co_GenerateNewLevel()
    {
        StartCoroutine(LevelManager.GetInstace.LoadLevel(LocalDataManager.currentLevel));
        yield return new WaitForSeconds(1f);
        Clear();
        GameCanvas.Create().AddTo(this);
        UIManager.Create().AddTo(this);
        player = (PlayerScripts)PlayerScripts.Create(GameObject.Find("RespawnPos").transform).AddTo(this);
    }
}
