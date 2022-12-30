using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class EventManager : MonoBehaviorInstance<EventManager>
{
    public VoidEventChannelSO OnNewGame;
    public VoidEventChannelSO OnStartGame;
    public VoidEventChannelSO OnContinuewGame;
    public VoidEventChannelSO OnPauseGame;
    public VoidEventChannelSO OnGenerateLevel;
    public VoidEventChannelSO OnGenerateLevelComplete;
    public VoidEventChannelSO OnLoseGame;
    public VoidEventChannelSO OnWinGame;

    public static EventManager Create(Transform parent = null)
    {
        return Instantiate<EventManager>(Resources.Load<EventManager>("Prefabs/Manager/EventManager"), parent);
    }

    private void OnDisable()
    {
        OnNewGame.OnEventRaise = null;
        OnStartGame.OnEventRaise = null;
        OnContinuewGame.OnEventRaise = null;
        OnPauseGame.OnEventRaise = null;
        OnGenerateLevel.OnEventRaise = null;
        OnGenerateLevelComplete.OnEventRaise = null;
    }
}
