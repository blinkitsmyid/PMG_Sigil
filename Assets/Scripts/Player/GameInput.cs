using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions _playerInputActions;
    public event EventHandler OnFlashlightToggle;
    public event EventHandler OnRunStarted;
    public event EventHandler OnRunCanceled;
    
    private void Awake()
    {
        Instance = this;

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        _playerInputActions.Player.Flashlight.performed += Flashlight_performed;
        _playerInputActions.Player.Run.started += Run_started;
        _playerInputActions.Player.Run.canceled += Run_canceled;
    }
    
    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector2 GetMousePosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }
    public void EnableMovement()
    {
        _playerInputActions.Enable();
    }
    public void DisableMovement()
    {
        _playerInputActions.Disable();
    }
    
    private void Flashlight_performed(InputAction.CallbackContext obj)
    {
        OnFlashlightToggle?.Invoke(this, EventArgs.Empty);
    }
  
    private void Run_started(InputAction.CallbackContext obj)
    {
        OnRunStarted?.Invoke(this, EventArgs.Empty);
        Debug.Log("Run started");
    }

    private void Run_canceled(InputAction.CallbackContext obj)
    {
        OnRunCanceled?.Invoke(this, EventArgs.Empty);
    }
}