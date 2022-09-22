using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
}
