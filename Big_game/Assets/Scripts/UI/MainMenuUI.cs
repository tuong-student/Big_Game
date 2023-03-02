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

        private void OnEnable()
        {

            continueButton.onClick.AddListener(ContinueGame);
            newGameButton.onClick.AddListener(NewGame);
            settingButton.onClick.AddListener(ToSetting);
            creditButton.onClick.AddListener(ToCreadit);
            exitButton.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
        
            continueButton.onClick.RemoveListener(ContinueGame);
            newGameButton.onClick.RemoveListener(NewGame);
            settingButton.onClick.RemoveListener(ToSetting);
            creditButton.onClick.RemoveListener(ToCreadit);
            exitButton.onClick.RemoveListener(Exit);

        }
        private void ContinueGame()
        {
            AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            GameCanvas.GetInstance.ActiveMenu(Menu.InGame);
        }
        private void NewGame()
        {
            AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            newGame.NewGame();
        }
        private void ToSetting()
        {
            GameCanvas.GetInstance.ActiveMenu(Menu.Setting);
            AudioManager.GetInstance.PlaySFX(sound.buttonClick);
        }

        private void ToCreadit()
        {
            gameObject.SetActive(false);
            creditsPanel.SetActive(true);
            AudioManager.GetInstance.PlaySFX(sound.buttonClick);
        }
        private void Exit()
        {

            AudioManager.GetInstance.PlaySFX(sound.buttonClick);
            Application.Quit();

        }

    }
}
