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
        AudioManager.Create();
        GameCanvas.Create();

        PoolingManager.Create().AddTo(this);
        ExplodeManager.Create().AddTo(this);
        UIManager.Create().AddTo(this);

        GameManager.OnStartGame += GenerateNewLevel;
        LevelManager.OnGenerateNewLevel += GenerateNewLevel;

        LocalDataManager.soundsetting = 1;
        LocalDataManager.musicsetting = 1;

        yield return new WaitForSeconds(1f);
        if (respawnPos == null) respawnPos = GameObject.Find("RespawnPos").transform;
        player = (PlayerScripts) PlayerScripts.Create(respawnPos).AddTo(this);
    }

    private void OnDisable()
    {
        LevelManager.OnGenerateNewLevel -= GenerateNewLevel;
        GameManager.OnStartGame -= GenerateNewLevel;
    }

    public void GenerateNewLevel()
    {
        StartCoroutine(Co_GenerateNewLevel());
    }

    private IEnumerator Co_GenerateNewLevel()
    {
        yield return new WaitForSeconds(1.2f);
        Clear();
        PoolingManager.Create().AddTo(this);
        ExplodeManager.Create().AddTo(this);
        UIManager.Create().AddTo(this);
        player = (PlayerScripts)PlayerScripts.Create(GameObject.Find("RespawnPos").transform).AddTo(this);
    }
}
