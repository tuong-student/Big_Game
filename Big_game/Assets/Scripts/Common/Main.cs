using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using NOOD;

public class Main : MonoBehaviorInstance<Main>
{
    Transform respawnPos;
    PlayerScripts player;


    private IEnumerator Start()
    {
        if (Camera.main != null) Destroy(Camera.main.gameObject);
        Instantiate(Resources.Load("Prefabs/Manager/_ObjectPool"), null);
        Instantiate(Resources.Load("Prefabs/Game/Player/Main Camera"), null);
        LocalDataManager.Load();
        
        LevelManager.Create();
        GoldManager.Create();
        WeaponManager.Create();
        GameManager.Create();

        PoolingManager.Create().AddTo(this);
        ExplodeManager.Create().AddTo(this);
        GameCanvas.Create().AddTo(this);
        UIManager.Create().AddTo(this);

        LevelManager.OnNextLevel += GenerateNewLevel;

        yield return new WaitForSeconds(1f);
        if (respawnPos == null) respawnPos = GameObject.Find("RespawnPos").transform;
        player = (PlayerScripts) PlayerScripts.Create(respawnPos).AddTo(this);
    }

    void OnDestroy()
    {
        
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
        PoolingManager.Create().AddTo(this);
        ExplodeManager.Create().AddTo(this);
        GameCanvas.Create().AddTo(this);
        UIManager.Create().AddTo(this);
        player = (PlayerScripts)PlayerScripts.Create(GameObject.Find("RespawnPos").transform).AddTo(this);
    }
}
