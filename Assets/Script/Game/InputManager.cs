using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ControlScheme
{
    PC,
    Gamepad
}

public class InputManager : MonoBehaviour
{

    [SerializeField] PlayerInput playerInput;

    public static event System.Action<Vector2, bool> OnMove;
    public static event System.Action OnJump;
    public static event System.Action<bool> OnAirAttack;
    public static event System.Action OnNormalAttack;
    public static event System.Action OnInteract;

    public static event System.Action<bool> OnActiveShoot;
    public static event System.Action<Vector2> OnShoot;

    public static ControlScheme CurrentScheme {get; private set;}

    public static System.Action<ControlScheme> onSchemeSwitch;

    string currenScheme;


    private void OnEnable()
    {
        playerInput.onActionTriggered += HandleInput;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= HandleInput;
    }

    private void Update()
    {
        CheckControlScheme();
    }

    public void HandleInput(InputAction.CallbackContext context)
    {

        TryInvokeMove(context);
        TryInvokeJump(context);
        TryInvokeAirAttack(context);
        TryInvokeNormalAttack(context);
        TryInteract(context);
        TryActiveShoot(context);
        TryInvokeShoot(context);
    }

    void CheckControlScheme()
    {
        if(playerInput.currentControlScheme == currenScheme) return;
        currenScheme = playerInput.currentControlScheme;

        if(currenScheme == "Gamepad")
        {
            CurrentScheme = ControlScheme.Gamepad;
        }
        else
        {
            CurrentScheme =ControlScheme.PC;
        }

        onSchemeSwitch?.Invoke(CurrentScheme);
    }

    void TryInvokeMove(InputAction.CallbackContext context)
    {
        if(context.action.name != "Move")return;

        
        Vector2 direction = context.ReadValue<Vector2>();
        if(context.started)
        {
            OnMove?.Invoke(direction, true);
        }
        if (context.performed)
        {
            OnMove?.Invoke(direction, true);
        }
        else if(context.canceled)
        {
            OnMove?.Invoke(direction, false);
        }
    }

    void TryInvokeJump(InputAction.CallbackContext context)
    {
        if(context.action.name != "Jump") return;

        if(context.started)
        {
            OnJump?.Invoke();
        }
    }

    void TryInvokeNormalAttack(InputAction.CallbackContext context)
    {
        if (context.action.name != "NormalAttack") return;

        if (context.started)
        {
            OnNormalAttack?.Invoke();
        }
    }

    void TryInvokeAirAttack(InputAction.CallbackContext context)
    {
        if (context.action.name != "AirAttack") return;

        if (context.started)
        {
            OnAirAttack?.Invoke(true);
        }

        if (context.canceled)
        {
            OnAirAttack?.Invoke(false);
        }
    }

    void TryInteract(InputAction.CallbackContext context)
    {
        if (context.action.name != "Interact") return;

        if (context.started)
        {
            OnInteract?.Invoke();
        }
    }

    void TryActiveShoot(InputAction.CallbackContext context)
    {
        if(context.action.name != "ActiveShoot") return;

        if (context.started)
        {
            OnActiveShoot?.Invoke(true);
        }
        if (context.canceled)
        {
            OnActiveShoot?.Invoke(false);
        }
    }

    void TryInvokeShoot(InputAction.CallbackContext context)
    {
        if (context.action.name != "DirectionShoot") return;

        if (context.performed)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            OnShoot?.Invoke(direction);
        }
    }

}
