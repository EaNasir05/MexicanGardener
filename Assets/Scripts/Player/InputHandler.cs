using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Action<Vector2> OnMoveInput;
    public Action OnPullPerform;
    public Action OnPullCancel;
    public Action OnPushPerform;
    public Action OnPushCancel;
    public Action OnSelectInput;

    public void OnMove(InputValue input)
    {
        OnMoveInput?.Invoke(input.Get<Vector2>());
    }

    public void OnPull(InputValue input)
    {
        if (input.isPressed)
            OnPullPerform?.Invoke();
        else
            OnPullCancel?.Invoke();
    }

    public void OnPush(InputValue input)
    {
        if (input.isPressed)
            OnPushPerform?.Invoke();
        else
            OnPushCancel?.Invoke();
    }
}
