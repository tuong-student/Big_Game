using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class LevelManager : MonoBehaviorInstance<LevelManager>
{
    public static Action OnNextLevel;
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
        Instantiate(mainMenuDungeon);
        ActiveMainMenuLevel();
        
        GameManager.OnStartGame += LoadCurrentLevel;
        GameManager.OnStartGame += DeactiveMainMenuLevel;
    }

    public void NextLevel()
    {
        LocalDataManager.currentLevel++;
        LocalDataManager.Save();
        OnNextLevel?.Invoke();
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
        LoadLevel(LocalDataManager.currentLevel);
    }

    public IEnumerator LoadLevel(int level)
    {
        if (!isFirstTime)
            GameManager.GetInstace.TransitionAnimation();
        isFirstTime = false;
        yield return new WaitForSeconds(0.5f);
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
        levelPortal.OpenAnimation();
    }

    public void ClosePortal()
    {
        levelPortal.CloseAnimation();
    }
}
