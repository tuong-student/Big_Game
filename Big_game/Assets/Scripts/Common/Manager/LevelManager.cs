using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviorInstance<LevelManager>
{
    public static Action OnNextLevel;
    [SerializeField] List<GameObject> levels;
    private List<GameObject> activeLevels = new List<GameObject>();


    public static LevelManager Create(Transform parent = null)
    {
        return Instantiate<LevelManager>(Resources.Load<LevelManager>("Prefabs/Manager/LevelManager"), parent);
    }

    private void Start()
    {
        LoadLevel(LocalDataManager.currentLevel);
    }

    public void NextLevel()
    {
        LocalDataManager.currentLevel++;
        LocalDataManager.Save();
        OnNextLevel?.Invoke();
    }

    public void LoadLevel(int level)
    {
        if (level <= 0) level = 1;
        if (level > levels.Count) level = levels.Count;
        LocalDataManager.currentLevel = level;
        LocalDataManager.Save();
        foreach (var lv in activeLevels)
        {
            if (lv) Destroy(lv.gameObject);
        }

        activeLevels.Add(Instantiate(levels[level - 1]));
    }
}