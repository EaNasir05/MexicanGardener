using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Action<Vector2> OnMoveInput;
    public Action OnPullInput;
    public Action OnPushInput;
    public Action OnSelectInput;

    public void OnMove(InputValue input)
    {
        Debug.Log("MOVE");
        OnMoveInput?.Invoke(input.Get<Vector2>());
    }

    public void OnPull(InputValue input)
    {
        Debug.Log("PULL");
    }

    public void OnPush(InputValue input)
    {
        Debug.Log("PUSH");
    }
}
