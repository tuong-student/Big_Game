using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static Action<Vector2> OnPlayerMove;
    public static Action<Vector3> OnMouseMove;
    public static Action<int> OnPlayerChangeGun;
    public static Action OnPlayerDash;
    public static Action OnPlayerWatchStats;
    public static Action OnPlayerShoot;
    public static Action OnPlayerPause;
    public static Action OnPlayerInteract;

    private GameInputSytem gameInputSystem;

    private void Awake()
    {
        gameInputSystem = new GameInputSytem();
        gameInputSystem.Player.Enable();

        gameInputSystem.Player.Watch_stats.performed += (InputAction.CallbackContext callback) =>
        {
            OnPlayerWatchStats?.Invoke();
        };
        gameInputSystem.Player.Pause.performed += (InputAction.CallbackContext callback) =>
        {
            OnPlayerPause?.Invoke();
        };
        gameInputSystem.Player.Dash.performed += (InputAction.CallbackContext callback) =>
        {
            OnPlayerDash?.Invoke();
        };
        gameInputSystem.Player.ChangGun1.performed += (InputAction.CallbackContext callback) =>
        {
            OnPlayerChangeGun?.Invoke(1);
        };
        gameInputSystem.Player.ChangGun2.performed += (InputAction.CallbackContext callback) =>
        {
            OnPlayerChangeGun?.Invoke(2);
        };
        gameInputSystem.Player.Interact.performed += (InputAction.CallbackContext callback) =>
        {
            OnPlayerInteract?.Invoke();
        };
    }

    private void Update()
    {
        Vector2 playerInput = gameInputSystem.Player.Movement.ReadValue<Vector2>();
        OnPlayerMove?.Invoke(playerInput);

        Vector2 mousePos = gameInputSystem.Player.MousePosition.ReadValue<Vector2>();
        OnMouseMove?.Invoke(mousePos);

        if(gameInputSystem.Player.Shoot.phase == InputActionPhase.Performed)
        {
            OnPlayerShoot?.Invoke();
        }
    }

    private void OnDisable()
    {
        OnPlayerMove = null;
        OnPlayerDash = null;
        OnMouseMove = null;
        OnPlayerWatchStats = null;
        OnPlayerShoot = null;
        OnPlayerPause = null;
        OnPlayerChangeGun = null;
    }
}
