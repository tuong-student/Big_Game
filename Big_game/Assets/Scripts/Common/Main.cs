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
        Instantiate(Resources.Load("Prefabs/Manager/_ObjectPool"), null);
        LocalDataManager.Load();

        LevelManager.Create();
        GoldManager.Create();
        PoolingManager.Create();
        ExplodeManager.Create();
        WeaponManager.Create();

        GameCanvas.Create().AddTo(this);
        GameManager.Create().AddTo(this);
        UIManager.Create().AddTo(this);

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
