using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;

public class GameCanvas : MonoBehaviorInstance<GameCanvas>
{
    public Text goldText;
    [SerializeField] GameObject mainMenu, inGameMenu, settingMenu, chooseCharacterMenu;
    bool isPauseOn = false;

    public static GameCanvas Create(Transform parent = null)
    {
        return Instantiate<GameCanvas>(Resources.Load<GameCanvas>("Prefabs/Canvas/GameCanvas"), parent);
    }

    private void Start()
    {
        if (LocalDataManager.playerNumber == 0)
            ActiveChooseCharacterMenu();
        else
        {
            EventManager.GetInstance.OnContinuewGame.RaiseEvent();
            ActiveInGameMenu();
        }

        EventManager.GetInstance.OnStartGame.OnEventRaise += ActiveInGameMenu;
        EventManager.GetInstance.OnPauseGame.OnEventRaise += () => 
	    {
            ActivePauseMenu();
	        isPauseOn = true;
	    };
        EventManager.GetInstance.OnContinuewGame.OnEventRaise += () => 
	    {
            ActiveInGameMenu();
	        isPauseOn = false; 
	    };
        EventManager.GetInstance.OnGenerateLevel.OnEventRaise += () =>
        {
            inGameMenu.SetActive(false);
        };
        EventManager.GetInstance.OnGenerateLevelComplete.OnEventRaise += () =>
        {
            inGameMenu.SetActive(true);
        };

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPauseOn)
                EventManager.GetInstance.OnPauseGame.OnEventRaise?.Invoke();
            else 
		        EventManager.GetInstance.OnContinuewGame.OnEventRaise?.Invoke();
        }
    }

    public UpgradePanel CreateUpgradePanel()
    {
        if (GameObject.FindObjectOfType<UpgradePanel>() != null) return null;
        return Instantiate<UpgradePanel>(Resources.Load<UpgradePanel>("Prefabs/Game/Upgrade/UpgradePanel"), this.transform);
    }

    public void ActiveInGameMenu()
    {
        ActiveMenu(Menu.InGame);
    }

    public void ActivePauseMenu()
    {
        ActiveMenu(Menu.Main);
    }

    public void ActiveSettingMenu()
    {
        ActiveMenu(Menu.Setting);
    }

    public void ActiveChooseCharacterMenu()
    {
        ActiveMenu(Menu.ChooseCharacter);
    }

    public void ActiveMenu(Menu menu)
    { 
        switch(menu)
        {
            case Menu.Main:
                mainMenu.SetActive(true);
                inGameMenu.SetActive(false);
                settingMenu.SetActive(false);
                chooseCharacterMenu.SetActive(false);
                break;
            case Menu.InGame:
                mainMenu.SetActive(false);
                inGameMenu.SetActive(true);
                settingMenu.SetActive(false);
                chooseCharacterMenu.SetActive(false);
                break;
            case Menu.Setting:
                mainMenu.SetActive(false);
                inGameMenu.SetActive(false);
                settingMenu.SetActive(true);
                chooseCharacterMenu.SetActive(false);
                break;
            case Menu.ChooseCharacter:
                mainMenu.SetActive(false);
                inGameMenu.SetActive(false);
                settingMenu.SetActive(false);
                chooseCharacterMenu.SetActive(true);
                break;
        }
    }
}


public enum Menu
{ 
    Main,
    InGame,
    Setting,
    ChooseCharacter
}
