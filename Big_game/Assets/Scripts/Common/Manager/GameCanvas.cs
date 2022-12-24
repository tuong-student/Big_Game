using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;

public class GameCanvas : MonoBehaviorInstance<GameCanvas>
{
    public Text goldText;
    [SerializeField] GameObject mainMenu, inGameMenu, settingMenu;

    public static GameCanvas Create(Transform parent = null)
    {
        return Instantiate<GameCanvas>(Resources.Load<GameCanvas>("Prefabs/Canvas/GameCanvas"), parent);
    }

    private void Start()
    {
        ActiveMenu(Menu.Main); 
    }

    public UpgradePanel CreateUpgradePanel()
    {
        if (GameObject.FindObjectOfType<UpgradePanel>() != null) return null;
        return Instantiate<UpgradePanel>(Resources.Load<UpgradePanel>("Prefabs/Game/Upgrade/UpgradePanel"), this.transform);
    }

    public void ActiveMenu(Menu menu)
    { 
        switch(menu)
        {
            case Menu.Main:
                mainMenu.SetActive(true);
                inGameMenu.SetActive(false);
                settingMenu.SetActive(false);
                break;
            case Menu.InGame:
                mainMenu.SetActive(false);
                inGameMenu.SetActive(true);
                settingMenu.SetActive(false);
                break;
            case Menu.Setting:
                mainMenu.SetActive(false);
                inGameMenu.SetActive(false);
                settingMenu.SetActive(true);
                break;
        }
    }

    public void SetGoldText(string text)
    {
        goldText.text = text;
    }
    private void Awake()
    {
        LocalDataManager.Load();

    }
}


public enum Menu
{ 
    Main,
    InGame,
    Setting
}
