using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class LevelManager : MonoBehaviorInstance<LevelManager>
{
    public static Action OnGenerateNewLevel;
    [SerializeField] List<GameObject> levels;
    [SerializeField] GameObject mainMenuDungeon;
    private List<GameObject> activeLevels = new List<GameObject>();
    bool isFirstTime = true;
    Portal levelPortal;

    public static LevelManager Create(Transform parent = null)
    {
        return Instantiate<LevelManager>(Resources.Load<LevelManager>("Prefabs/Manager/LevelManager"), parent);
    }

    private void Start()
    {
        mainMenuDungeon = Instantiate(mainMenuDungeon);
        ActiveMainMenuLevel();

        GameManager.OnStartGame += LoadCurrentLevel;
        GameManager.OnStartGame += DeactiveMainMenuLevel;
        OnGenerateNewLevel += LoadCurrentLevel;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= LoadCurrentLevel;
        GameManager.OnStartGame -= DeactiveMainMenuLevel;
        OnGenerateNewLevel -= LoadCurrentLevel;
        GameManager.OnStartGame = null;
    }

    public void NextLevel()
    {
        LocalDataManager.currentLevel++;
        LocalDataManager.Save();
        OnGenerateNewLevel?.Invoke();
    }

    public void ActiveMainMenuLevel()
    {
        mainMenuDungeon.SetActive(true);
    }

    public void DeactiveMainMenuLevel()
    {
        mainMenuDungeon.SetActive(false);
    }

    public void LoadCurrentLevel()
    {
        StartCoroutine(LoadLevel(LocalDataManager.currentLevel));
    }

    public IEnumerator LoadLevel(int level)
    {
        if (!isFirstTime)
            GameManager.GetInstance.TransitionAnimation();
        isFirstTime = false;
        yield return new WaitForSeconds(1f);
        if (level <= 0) level = 1;
        if (level > levels.Count) level = levels.Count;

        LocalDataManager.currentLevel = level;
        LocalDataManager.Save();

        foreach (var lv in activeLevels)
        {
            if (lv) Destroy(lv.gameObject);
        }

        activeLevels.Add(Instantiate(levels[level - 1]));
        levelPortal = FindObjectOfType<Portal>();
    }

    public void OpenPortal()
    {
        if(levelPortal != null)
        { 
            AudioManager.GetInstance.PlaySFX(sound.telePortal);
            levelPortal.OpenAnimation();
	    }
    }

    public void ClosePortal()
    {
        if(levelPortal != null)
            levelPortal.CloseAnimation();
    }
}
