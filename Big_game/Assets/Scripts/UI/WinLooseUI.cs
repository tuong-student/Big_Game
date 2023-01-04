using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;

public class WinLooseUI : MonoBehaviorInstance<WinLooseUI>
{
    [SerializeField] private CanvasGroup winPanel, loosePanel;
    [SerializeField] private Button tryAgainButton, quitButton, playAgainButton, restartGameButton;
    [SerializeField] private ResetUI reset;

    private void Start()
    {
        WinPanelUnactivate();
        LoosePanelUnactivate();
        EventManager.GetInstance.OnLoseGame.OnEventRaise += () =>
        {
            NoodyCustomCode.StartDelayFunction(LoosePanelActivate, 0.2f);
        };
        EventManager.GetInstance.OnWinGame.OnEventRaise += () =>
        {
            NoodyCustomCode.StartDelayFunction(WinPanelActivate, 0.2f);
        };
    }

    private void OnEnable()
    {
        tryAgainButton.onClick.AddListener(TryAgainFromLevel1);
        quitButton.onClick.AddListener(ExitGame);
        playAgainButton.onClick.AddListener(PlayAgainFromPickChar);
        restartGameButton.onClick.AddListener(PlayAgainFromPickChar);
    }

    private void OnDisable()
    {
        tryAgainButton.onClick.RemoveListener(TryAgainFromLevel1);
        quitButton.onClick.RemoveListener(ExitGame);
        playAgainButton.onClick.RemoveListener(PlayAgainFromPickChar);
        restartGameButton.onClick.RemoveListener(PlayAgainFromPickChar);
    }


    public void WinPanelActivate()
    {
        winPanel.alpha = 1;
        winPanel.interactable = true;
        winPanel.blocksRaycasts = true;
    }
    public void LoosePanelActivate()
    {
        loosePanel.alpha = 1;
        loosePanel.interactable = true;
        loosePanel.blocksRaycasts = true;

    }
    public void WinPanelUnactivate()
    {
        winPanel.alpha = 0;
        winPanel.interactable = false;
        winPanel.blocksRaycasts = false;

    }
    public void LoosePanelUnactivate()
    {
        loosePanel.alpha = 0;
        loosePanel.interactable = false;
        loosePanel.blocksRaycasts = false;

    }

    private void TryAgainFromLevel1()
    {
        AudioManager.GetInstance.PlaySFX(sound.buttonClick);
        LoosePanelUnactivate();
        reset.TryAgain();
    }
    private void ExitGame()
    {
        AudioManager.GetInstance.PlaySFX(sound.buttonClick);
        Application.Quit();
    }
    private void PlayAgainFromPickChar()
    {
        AudioManager.GetInstance.PlaySFX(sound.buttonClick);
        if (loosePanel.alpha == 1)
        {
            LoosePanelUnactivate();
        }
        else
        {
            WinPanelUnactivate();
        }
        reset.NewGame();
    }
}

