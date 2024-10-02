using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;

using static PlayerIA;

[CreateAssetMenu(fileName = "InputReader", menuName = "Zelda/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction Click = delegate { };

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
            if (context.started)
            {
                Debug.Log($"started : {context.ReadValue<Vector2>()}");
            }
            else
            {
                Debug.Log($"performed : {context.ReadValue<Vector2>()}");
            }
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
}