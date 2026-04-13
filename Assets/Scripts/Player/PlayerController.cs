using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float pullForce;
    [SerializeField] private float pushForce;
    private Rigidbody _rb;
    private InputHandler _inputHandler;
    private Vector2 moveInput;
    private bool movingHorizontally = false;
    private bool movingVertically = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Start()
    {
        _inputHandler.OnMoveInput += OnMoveInput;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3(moveInput.x * movementSpeed, moveInput.y * movementSpeed, 0);
    }

    private void OnMoveInput(Vector2 input)
    {
        if (input.x != 0 && !movingVertically)
        {
            movingHorizontally = true;
            input.y = 0;
        }
        if (input.y != 0 && !movingHorizontally)
        {
            movingVertically = true;
            input.x = 0;
        }

        if (input.x == 0 && movingHorizontally)
            movingHorizontally = false;
        if (input.y == 0 && movingVertically)
            movingVertically = false;

        moveInput = input;
    }
}
