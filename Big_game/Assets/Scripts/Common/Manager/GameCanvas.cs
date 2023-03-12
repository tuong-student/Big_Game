using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;
using Game.UI.Support;

namespace Game.UI
{
    public class GameCanvas : MonoBehaviorInstance<GameCanvas>
    {
        [SerializeField] GameObject mainMenu, inGameMenu, settingMenu, chooseCharacterMenu, winLoseMenu, debugMenu;
        

        public static GameCanvas Create(Transform parent = null)
        {
            return Instantiate<GameCanvas>(Resources.Load<GameCanvas>("Prefabs/Canvas/GameCanvas"), parent);
        }

        private void Awake()
        {
            EventManager.GetInstance.OnContinuewGame.OnEventRaise += () =>
            {
                ActiveInGameMenu();
            };
        }

        private void Start()
        {
            EventManager.GetInstance.OnStartGame.OnEventRaise += ActiveInGameMenu;
            EventManager.GetInstance.OnPauseGame.OnEventRaise += () =>
            {
                ActivePauseMenu();
            };
            
            EventManager.GetInstance.OnGenerateLevel.OnEventRaise += () =>
            {
                inGameMenu.SetActive(false);
            };
            EventManager.GetInstance.OnGenerateLevelComplete.OnEventRaise += (int number) =>
            {
                inGameMenu.SetActive(true);
            };
            EventManager.GetInstance.OnWinGame.OnEventRaise += ActiveWinLoseMenu;
            EventManager.GetInstance.OnLoseGame.OnEventRaise += ActiveWinLoseMenu;

            EventManager.GetInstance.OnDebugEnable.OnEventRaise += ActiveDebugMenu;
            EventManager.GetInstance.OnDebugDisable.OnEventRaise += HideDebugMenu;
        }

        private void Update()
        {

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

        public void ActiveWinLoseMenu()
        {
            EventManager.GetInstance.OnTurnOnUI.RaiseEvent();
            ActiveMenu(Menu.WinLose);
        }

        public void ActiveDebugMenu()
        {
            debugMenu.gameObject.SetActive(true);
        }

        public void HideDebugMenu()
        {
            debugMenu.gameObject.SetActive(false);
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
                    winLoseMenu.SetActive(false);
                    break;
                case Menu.InGame:
                    mainMenu.SetActive(false);
                    inGameMenu.SetActive(true);
                    settingMenu.SetActive(false);
                    chooseCharacterMenu.SetActive(false);
                    winLoseMenu.SetActive(false);
                    break;
                case Menu.Setting:
                    mainMenu.SetActive(false);
                    inGameMenu.SetActive(false);
                    settingMenu.SetActive(true);
                    chooseCharacterMenu.SetActive(false);
                    winLoseMenu.SetActive(false);
                    break;
                case Menu.ChooseCharacter:
                    mainMenu.SetActive(false);
                    inGameMenu.SetActive(false);
                    settingMenu.SetActive(false);
                    chooseCharacterMenu.SetActive(true);
                    winLoseMenu.SetActive(false);
                    break;
                case Menu.WinLose:
                    mainMenu.SetActive(false);
                    inGameMenu.SetActive(false);
                    settingMenu.SetActive(false);
                    chooseCharacterMenu.SetActive(false);
                    winLoseMenu.SetActive(true);
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
        WinLose
    }
}
