using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour , PlayerControls.IPlayerActionMapActions
{
    public Vector2 MovementValue { get; private set; }
    public bool BasicAttack { get; private set; }
    public bool IsBlocking { get; private set; }
    public bool IsCrouching { get; private set; }
    public event Action JumpEvent;
    public event Action QSpecialAttack;

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.PlayerActionMap.SetCallbacks(this);
        _playerControls.PlayerActionMap.Enable();
    }
    
    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnNormalAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BasicAttack = true;
        }
        else if (context.canceled)
        {
            BasicAttack = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    public void OnQSpecial(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        
        QSpecialAttack?.Invoke();
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsBlocking = true;
        }
        else
        {
            IsBlocking = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsCrouching = true;
        }
        else
        {
            IsCrouching = false;
        }
    }

    private void OnDestroy()
    {
        _playerControls.PlayerActionMap.Disable();
    }
}
