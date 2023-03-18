using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NOOD;


public class EventManager : MonoBehaviorInstance<EventManager>
{
    //--- Player Event region ---//
    #region PlayerEvent
    public EventHandler OnPlayerCreate;
    #endregion

    //-------/

    public VoidEventChannelSO OnNewGame;
    public VoidEventChannelSO OnStartGame;
    public VoidEventChannelSO OnContinuewGame;
    public VoidEventChannelSO OnPauseGame;
    public VoidEventChannelSO OnGenerateLevel;
    public VoidEventChannelSO OnLoseGame;
    public VoidEventChannelSO OnWinGame;
    public VoidEventChannelSO OnTryAgain;
    public VoidEventChannelSO OnTurnOnUI;
    public VoidEventChannelSO OnDebugEnable;
    public VoidEventChannelSO OnDebugDisable;

    public VoidIntEventChannelSO OnGenerateLevelComplete;

    private bool isPause = false;

    public static EventManager Create(Transform parent = null)
    {
        EventManager temp = GameObject.FindObjectOfType<EventManager>();
        if (temp != null) return temp; 
        return Instantiate<EventManager>(Resources.Load<EventManager>("Prefabs/Manager/EventManager"), parent);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        GameInput.OnPlayerPause += () =>
        {
            isPause = !isPause;
            if (isPause)
                OnPauseGame.RaiseEvent();
            else
                OnContinuewGame.RaiseEvent();
        };
    }

    protected override void Dispose()
    {
        OnNewGame.OnEventRaise = null;
        OnStartGame.OnEventRaise = null;
        OnContinuewGame.OnEventRaise = null;
        OnPauseGame.OnEventRaise = null;
        OnGenerateLevel.OnEventRaise = null;
        OnGenerateLevelComplete.OnEventRaise = null;
        OnWinGame.OnEventRaise = null;
        OnLoseGame.OnEventRaise = null;
        OnTryAgain.OnEventRaise = null;
        OnTurnOnUI.OnEventRaise = null;
        OnDebugEnable.OnEventRaise = null;
        OnDebugDisable.OnEventRaise = null;
        OnPlayerCreate = null;
    }
}
