using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using NOOD;

public class Main : MonoBehaviorInstance<Main>
{
    Transform respawnPos;
    PlayerScripts player;


    private void Start()
    {
        GameManager.Create();
        //SelectCharacter();
        PlayGame();
    }

    public void SelectCharacter()
    {
        StartCoroutine(CO_SelectCharacter());
    }

    public void PlayGame()
    {
        StartCoroutine(CO_PlayGame());
    }

    public IEnumerator CO_SelectCharacter()
    {
        GameManager.GetInstance.TransitionAnimation();
        yield return new WaitForSeconds(1f); 
        Clear();
        ChooseCharacterManager.Create().AddTo(this);
    }

    public IEnumerator CO_PlayGame()
    {
        GameManager.GetInstance.TransitionAnimation();
        yield return new WaitForSeconds(1f);
        Clear();
        if (Camera.main != null) Destroy(Camera.main.gameObject);
        Instantiate(Resources.Load("Prefabs/Manager/_ObjectPool"), null);
        Instantiate(Resources.Load("Prefabs/Game/Player/Main Camera"), null);
        LocalDataManager.LoadInit();
        if(LocalDataManager.isSaveBefore == 1)
        {
            LocalDataManager.Load();
	    }

        EventManager.Create();
        LevelManager.Create();
        GoldManager.Create();
        WeaponManager.Create();
        AudioManager.Create();
        GameCanvas.Create();

        PoolingManager.Create().AddTo(this);
        ExplodeManager.Create().AddTo(this);
        UIManager.Create().AddTo(this);

        EventManager.GetInstance.OnStartGame.OnEventRaise += GenerateNewLevel;
        EventManager.GetInstance.OnGenerateLevel.OnEventRaise += GenerateNewLevel;
        EventManager.GetInstance.OnGenerateLevel.OnEventRaise += LocalDataManager.Save;
        EventManager.GetInstance.OnContinuewGame.OnEventRaise += SpawnPlayerIfNeed;

        LocalDataManager.soundsetting = 0;
        LocalDataManager.musicsetting = 0;
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

    private void SpawnPlayerIfNeed()
    { 
        if(respawnPos == null) respawnPos = GameObject.Find("RespawnPos").transform;
        if (GameObject.FindObjectOfType<PlayerScripts>() == null)
        { 
            player = (PlayerScripts)PlayerScripts.Create();
            player.transform.position = respawnPos.transform.position;
	    }
    }
}
