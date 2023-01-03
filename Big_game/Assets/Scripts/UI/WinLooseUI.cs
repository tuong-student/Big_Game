using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLooseUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup winPanel, loosePanel;
    [SerializeField] private Button tryAgainButton, quitButton, playAgainButton, restartGameButton;
    [SerializeField] private ResetUI reset;

    private void Start()
    {
        WinPanelUnactivate();
        LoosePanelUnactivate();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            WinPanelActivate();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoosePanelActivate();
        }
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

