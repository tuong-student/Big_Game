using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> levels;
    public static GameManager Create(Transform parent = null)
    {
        return Instantiate<GameManager>(Resources.Load<GameManager>("Prefabs/Manager/GameManger"), parent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameCanvas.i.CreateUpgradePanel();
        }
    }

    public void OpenSetting()
    {
        SettingManager.Create();
    }
    
    public void NextLevel()
    {
        LocalDataManager.currentLevel++;
        for(int i = 0; i < levels.Count; i++)
        {
            if (i != LocalDataManager.currentLevel - 1) levels[i].SetActive(false);
            else levels[i].SetActive(true);
        }
    }
}
