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
        
        EventManager.Create();
        LevelManager.Create();
        GoldManager.Create();
        WeaponManager.Create();
        GameManager.Create();
        AudioManager.Create();
        GameCanvas.Create();

        PoolingManager.Create().AddTo(this);
        ExplodeManager.Create().AddTo(this);
        UIManager.Create().AddTo(this);

        EventManager.GetInstance.OnStartGame.OnEventRaise += GenerateNewLevel;
        EventManager.GetInstance.OnGenerateLevel.OnEventRaise += GenerateNewLevel;

        LocalDataManager.soundsetting = 0;
        LocalDataManager.musicsetting = 0;

        yield return new WaitForSeconds(1f);
        if (respawnPos == null) respawnPos = GameObject.Find("RespawnPos").transform;
        player = (PlayerScripts)PlayerScripts.Create();
        player.transform.position = respawnPos.transform.position;
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
        respawnPos = GameObject.Find("RespawnPos").transform;
        player.transform.position = respawnPos.transform.position;
        EventManager.GetInstance.OnGenerateLevelComplete.OnEventRaise?.Invoke();
    }
}
