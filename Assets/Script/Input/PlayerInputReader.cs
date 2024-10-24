using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;

using static PlayerIA;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Zelda/PlayerInputReader")]
public class PlayerInputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction Click = delegate { };
    public event UnityAction ChangeWeapon = delegate { };
    public event UnityAction ChangeShield = delegate { };
    public event UnityAction StartInteraction = delegate { };
    public event UnityAction CancelInteraction= delegate { };
    public event UnityAction InventoryToggle = delegate { };

    PlayerIA inputActions;

    public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();

    void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerIA();
            inputActions.Player.SetCallbacks(this);
        }
    }

    public void EnablePlayerActions()
    {
        inputActions.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }
        else
        {
            Move.Invoke(Vector2.zero);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Click.Invoke();
        }
    }

    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            ChangeWeapon.Invoke();
        }
    }

    public void OnChangeShield(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            ChangeShield.Invoke();
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            StartInteraction.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            CancelInteraction.Invoke();
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            StartInteraction.Invoke();
        }
    }
}