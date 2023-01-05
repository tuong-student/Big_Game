using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using NOOD;
using NOOD.NoodCamera;

public class Main : MonoBehaviorInstance<Main>
{
    Transform respawnPos;
    PlayerScripts player;
    CameraFollow mainCamera;

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
        mainCamera = Instantiate(Resources.Load<CameraFollow>("Prefabs/Game/Player/Main Camera"), null);
        LocalDataManager.LoadInit();
        if(LocalDataManager.isSaveBefore == 1)
        {
            LocalDataManager.Load();
	    }

        DontDestroyOnLoad(EventManager.Create().gameObject);
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
        EventManager.GetInstance.OnNewGame.OnEventRaise += NewGame;
        EventManager.GetInstance.OnTryAgain.OnEventRaise += () =>
        {
            NoodyCustomCode.StartDelayFunction(SpawnPlayerIfNeed, 1.2f); 
        };

        LocalDataManager.soundsetting = 1;
        LocalDataManager.musicsetting = 1;
        yield return new WaitForSeconds(1.2f);
        if(LocalDataManager.playerNumber == 0)
            GameCanvas.GetInstance.ActiveChooseCharacterMenu();
    }

    public void NewGame()
    {
        StartCoroutine(CO_NewGame());
    }

    public IEnumerator CO_NewGame()
    {
        GameCanvas.GetInstance.DeactiveAllMenu();
        GameManager.GetInstance.TransitionAnimation();
        yield return new WaitForSeconds(1f);
        Clear();
        mainCamera.transform.position = new UnityEngine.Vector3(0, 0, -10);
        LevelManager.GetInstance.ActiveMainMenuLevel();

        PoolingManager.Create().AddTo(this);
        ExplodeManager.Create().AddTo(this);
        UIManager.Create().AddTo(this);

        yield return new WaitForSeconds(1.2f);
        GameCanvas.GetInstance.ActiveChooseCharacterMenu();
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
    }

    private void SpawnPlayerIfNeed()
    { 
        if(respawnPos == null) respawnPos = GameObject.Find("RespawnPos").transform;
        if (GameObject.FindObjectOfType<PlayerScripts>() == null && LocalDataManager.playerNumber != 0)
        { 
            player = (PlayerScripts)PlayerScripts.Create();
            player.transform.position = respawnPos.transform.position;
	    }
    }
}
