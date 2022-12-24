using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        AudioManager.Instance.PlaySFX(sound.buttonClick);
        gameObject.SetActive(false);
        inGamePanel.SetActive(true);
    }
    private void NewGame()
    {

        AudioManager.Instance.PlaySFX(sound.buttonClick);
        newGame.NewGame();
    }
    private void ToSetting()
    {
        gameObject.SetActive(false);
        settingPanel.SetActive(true);
        AudioManager.Instance.PlaySFX(sound.buttonClick);
    }

    private void ToCreadit()
    {
        gameObject.SetActive(false);
        creditsPanel.SetActive(true);
        AudioManager.Instance.PlaySFX(sound.buttonClick);


    }
    private void Exit()
    {

        AudioManager.Instance.PlaySFX(sound.buttonClick);
        Application.Quit();

    }

}
