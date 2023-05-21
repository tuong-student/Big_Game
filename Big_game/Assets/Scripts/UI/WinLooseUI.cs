using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;
using Game.Common.Interface;

public class WinLooseUI : MonoBehaviour, ISingleton
{
    [SerializeField] private GameObject winPanel, loosePanel;
    [SerializeField] private Button tryAgainButton, quitButton, playAgainButton, restartGameButton;
    [SerializeField] private ResetUI reset;

    private EventManager eventManager;
    private AudioManager audioManager;

    private void Start()
    {
        eventManager = SingletonContainer.Resolve<EventManager>();
        audioManager = SingletonContainer.Resolve<AudioManager>();
        WinPanelDeactivate();
        LoosePanelDeactivate();
        eventManager.OnLoseGame.OnEventRaise += () =>
        {
            NoodyCustomCode.StartDelayFunction(LoosePanelActivate, 1f);
        };
        eventManager.OnWinGame.OnEventRaise += () =>
        {
            NoodyCustomCode.StartDelayFunction(WinPanelActivate, 0.2f);
        };
        eventManager.OnTryAgain.OnEventRaise += () =>
        {
            NoodyCustomCode.StartDelayFunction(TryAgainFromLevel1, 0.2f);
        };
        eventManager.OnRestartGame.OnEventRaise += () =>
        {
            SingletonContainer.Resolve<Game.Common.Manager.GameManager>().RestartGame();
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
        winPanel.SetActive(true);
    }
    public void LoosePanelActivate()
    {
        Debug.Log("Active");
        loosePanel.SetActive(true);
    }
    public void WinPanelDeactivate()
    {
        winPanel.SetActive(false);
    }
    public void LoosePanelDeactivate()
    {
        loosePanel.SetActive(false);
    }

    private void TryAgainFromLevel1()
    {
        audioManager.PlaySFX(sound.buttonClick);
        LoosePanelDeactivate();
        reset.TryAgain();
    }
    private void ExitGame()
    {
        audioManager.PlaySFX(sound.buttonClick);
        Application.Quit();
    }
    private void PlayAgainFromPickChar()
    {
        audioManager.PlaySFX(sound.buttonClick);
        if (loosePanel.activeInHierarchy)
        {
            LoosePanelDeactivate();
        }
        else
        {
            WinPanelDeactivate();
        }
        reset.NewGame();
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

