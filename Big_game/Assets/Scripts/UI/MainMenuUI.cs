using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button continueButton, newGameButton, settingButton, creditButton, exitButton;
    [SerializeField] public GameObject settingPanel,inGamePanel;


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

        AudioManager.Instance.PlaySFX("ButtonClick");
        gameObject.SetActive(false);
        inGamePanel.SetActive(true);
    }
    private void NewGame()
    {

        AudioManager.Instance.PlaySFX("ButtonClick");

    }
    private void ToSetting()
    {
        gameObject.SetActive(false);
        settingPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("ButtonClick");
    }

    private void ToCreadit()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");

    }
    private void Exit()
    {

        AudioManager.Instance.PlaySFX("ButtonClick");

    }

}
