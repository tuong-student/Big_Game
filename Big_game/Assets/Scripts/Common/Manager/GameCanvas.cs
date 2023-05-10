using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;
using Game.UI.Support;

namespace Game.UI
{
    public class GameCanvas : MonoBehaviour, Game.Common.Interface.ISingleton
    {
        [SerializeField] GameObject mainMenu, inGameMenu, settingMenu, chooseCharacterMenu, winLoseMenu, debugMenu;

        private EventManager eventManager;

        public static GameCanvas Create(Transform parent = null)
        {
            return Instantiate<GameCanvas>(Resources.Load<GameCanvas>("Prefabs/Canvas/GameCanvas"), parent);
        }

        private void Awake()
        {
            RegisterToContainer();
        }

        private void Start()
        {
            eventManager = SingletonContainer.Resolve<EventManager>();
            eventManager.OnStartGame.OnEventRaise += ActiveInGameMenu;
            eventManager.OnContinueGame.OnEventRaise += ActiveInGameMenu;
            eventManager.OnPauseGame.OnEventRaise += () =>
            {
                ActivePauseMenu();
            };
            
            eventManager.OnGenerateLevel.OnEventRaise += () =>
            {
                inGameMenu.SetActive(false);
            };
            eventManager.OnGenerateLevelComplete.OnEventRaise += (int number) =>
            {
                inGameMenu.SetActive(true);
            };
            eventManager.OnWinGame.OnEventRaise += ActiveWinLoseMenu;
            eventManager.OnLoseGame.OnEventRaise += ActiveWinLoseMenu;

            eventManager.OnDebugEnable.OnEventRaise += ActiveDebugMenu;
            eventManager.OnDebugDisable.OnEventRaise += HideDebugMenu;
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
            eventManager.OnTurnOnUI.RaiseEvent();
            ActiveMenu(Menu.Main);
        }

        public void ActiveSettingMenu()
        {
            eventManager.OnTurnOnUI.RaiseEvent();
            ActiveMenu(Menu.Setting);
        }

        public void ActiveChooseCharacterMenu()
        {
            ActiveMenu(Menu.ChooseCharacter);
        }

        public void ActiveWinLoseMenu()
        {
            eventManager.OnTurnOnUI.RaiseEvent();
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

        public void DeactivateAllMenu()
        {
            mainMenu.SetActive(false);
            inGameMenu.SetActive(false);
            settingMenu.SetActive(false);
            chooseCharacterMenu.SetActive(false);
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnregisterToContainer()
        {
            SingletonContainer.UnRegister(this);
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
