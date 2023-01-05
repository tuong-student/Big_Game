using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;

public class GameCanvas : MonoBehaviorInstance<GameCanvas>
{
    public Text goldText;
    [SerializeField] GameObject mainMenu, inGameMenu, settingMenu, chooseCharacterMenu, popupMenu;
    bool isPauseOn = false;

    public static GameCanvas Create(Transform parent = null)
    {
        return Instantiate<GameCanvas>(Resources.Load<GameCanvas>("Prefabs/Canvas/GameCanvas"), parent);
    }

    private void Start()
    {
        EventManager.GetInstance.OnContinuewGame.RaiseEvent();
        ActiveInGameMenu();

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
            NoodyCustomCode.StartDelayFunction(() => { CreateUpgradePanel(); }, 0.8f);
        };
        EventManager.GetInstance.OnWinGame.OnEventRaise += ActivePopupMenu;
        EventManager.GetInstance.OnLoseGame.OnEventRaise += ActivePopupMenu;

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
        EventManager.GetInstance.OnTurnOnUI.RaiseEvent();
        if (GameObject.FindObjectOfType<UpgradePanel>() != null) return null;
        return Instantiate<UpgradePanel>(Resources.Load<UpgradePanel>("Prefabs/Game/Upgrade/UpgradePanel"), this.transform);
    }

    public void ActiveInGameMenu()
    {
        ActiveMenu(Menu.InGame);
    }

    public void ActivePauseMenu()
    {
        EventManager.GetInstance.OnTurnOnUI.RaiseEvent();
        ActiveMenu(Menu.Main);
    }

    public void ActiveSettingMenu()
    {
        EventManager.GetInstance.OnTurnOnUI.RaiseEvent();
        ActiveMenu(Menu.Setting);
    }

    public void ActiveChooseCharacterMenu()
    {
        ActiveMenu(Menu.ChooseCharacter);
    }

    public void ActivePopupMenu()
    {
        EventManager.GetInstance.OnTurnOnUI.RaiseEvent();
        ActiveMenu(Menu.Popup);
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
                //popupMenu.SetActive(false);
                break;
            case Menu.InGame:
                mainMenu.SetActive(false);
                inGameMenu.SetActive(true);
                settingMenu.SetActive(false);
                chooseCharacterMenu.SetActive(false);
                //popupMenu.SetActive(false);
                break;
            case Menu.Setting:
                mainMenu.SetActive(false);
                inGameMenu.SetActive(false);
                settingMenu.SetActive(true);
                chooseCharacterMenu.SetActive(false);
                //popupMenu.SetActive(false);
                break;
            case Menu.ChooseCharacter:
                mainMenu.SetActive(false);
                inGameMenu.SetActive(false);
                settingMenu.SetActive(false);
                chooseCharacterMenu.SetActive(true);
                //popupMenu.SetActive(false);
                break;
            case Menu.Popup:
                mainMenu.SetActive(false);
                inGameMenu.SetActive(false);
                settingMenu.SetActive(false);
                chooseCharacterMenu.SetActive(false);
                //popupMenu.SetActive(true);
                break;
        }
    }

    public void DeactiveAllMenu()
    {
        mainMenu.SetActive(false);
        inGameMenu.SetActive(false);
        settingMenu.SetActive(false);
        chooseCharacterMenu.SetActive(false);
    }
}


public enum Menu
{ 
    Main,
    InGame,
    Setting,
    ChooseCharacter,
    Popup
}
