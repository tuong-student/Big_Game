using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainMenuUI : MonoBehaviour
    {

        [SerializeField] private Button continueButton, newGameButton, settingButton, creditButton, exitButton;
        [SerializeField] public GameObject settingPanel,inGamePanel,creditsPanel;

        [SerializeField] private ResetUI newGame;

        void Start()
        {
            SingletonContainer.Resolve<EventManager>().OnContinueGame.OnEventRaise += ContinueGame;
        }

        private void OnEnable()
        {
            SingletonContainer.Resolve<EventManager>().OnContinueGame.OnEventRaise -= ContinueGame;
            newGameButton.onClick.AddListener(NewGame);
            settingButton.onClick.AddListener(ToSetting);
            creditButton.onClick.AddListener(ToCredit);
            exitButton.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
        
            newGameButton.onClick.RemoveListener(NewGame);
            settingButton.onClick.RemoveListener(ToSetting);
            creditButton.onClick.RemoveListener(ToCredit);
            exitButton.onClick.RemoveListener(Exit);

        }
        private void ContinueGame()
        {
            //AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            SingletonContainer.Resolve<GameCanvas>().ActiveMenu(Menu.InGame);
        }
        private void NewGame()
        {
            //AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            newGame.NewGame();
        }
        private void ToSetting()
        {
            //AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            SingletonContainer.Resolve<GameCanvas>().ActiveMenu(Menu.Setting);
        }

        private void ToCredit()
        {
            gameObject.SetActive(false);
            creditsPanel.SetActive(true);
            //AudioManager.GetInstance.PlaySFX(sound.buttonClick);
        }
        private void Exit()
        {

            //AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            Application.Quit();

        }

    }
}
