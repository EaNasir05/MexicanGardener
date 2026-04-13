using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum PlayerDirection { Left, Right, Up, Down }

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float pullForceStrength;
    [SerializeField] private float pushForceStrength;
    public bool unlockedPull;
    public bool unlockedPush;

    [SerializeField] private GameObject _pushTrigger;
    [SerializeField] private GameObject _pullTrigger;
    [SerializeField] private Transform[] _triggersPositions;
    private Puller _puller;
    private Pusher _pusher;
    private Rigidbody _rb;
    private InputHandler _inputHandler;

    private Vector2 moveInput;
    private bool movingHorizontally = false;
    private bool movingVertically = false;
    private bool pulling = false;
    private bool pushing = false;
    private PlayerDirection playerDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
        _puller = _pullTrigger.GetComponent<Puller>();
        _pusher = _pushTrigger.GetComponent<Pusher>();
    }

    private void Start()
    {
        _puller.forceStrength = pullForceStrength;
        _pusher.forceStrength = pushForceStrength;
        _inputHandler.OnMoveInput += OnMoveInput;
        _inputHandler.OnPullPerform += OnPullPerform;
        _inputHandler.OnPushPerform += OnPushPerform;
        _inputHandler.OnPushCancel += OnPushCancel;
        _inputHandler.OnPullCancel += OnPullCancel;
    }

    private void OnDestroy()
    {
        _inputHandler.OnMoveInput -= OnMoveInput;
        _inputHandler.OnPullPerform -= OnPullPerform;
        _inputHandler.OnPushPerform -= OnPushPerform;
        _inputHandler.OnPushCancel -= OnPushCancel;
        _inputHandler.OnPullCancel -= OnPullCancel;
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

        UpdatePlayerDirection(input);

        moveInput = input;
    }

    private void UpdatePlayerDirection(Vector2 input)
    {
        float x = input.x;
        float y = input.y;
        if (x > 0)
        {
            if (playerDirection != PlayerDirection.Right)
            {
                playerDirection = PlayerDirection.Right;
                _pusher.forceDirection = Vector2.right;
                _puller.forceDirection = Vector2.left;
                Quaternion rot = Quaternion.Euler(0, 0, 90);
                Vector3 pos = _triggersPositions[1].position;
                _pushTrigger.transform.position = pos;
                _pushTrigger.transform.rotation = rot;
                _pullTrigger.transform.position = pos;
                _pullTrigger.transform.rotation = rot;
            }
        }
        else if (x < 0)
        {
            if (playerDirection != PlayerDirection.Left)
            {
                playerDirection = PlayerDirection.Left;
                _pusher.forceDirection = Vector2.left;
                _puller.forceDirection = Vector2.right;
                Quaternion rot = Quaternion.Euler(0, 0, 90);
                Vector3 pos = _triggersPositions[3].position;
                _pushTrigger.transform.position = pos;
                _pushTrigger.transform.rotation = rot;
                _pullTrigger.transform.position = pos;
                _pullTrigger.transform.rotation = rot;
            }
        }
        else if (y > 0)
        {
            if (playerDirection != PlayerDirection.Up)
            {
                playerDirection = PlayerDirection.Up;
                _pusher.forceDirection = Vector2.up;
                _puller.forceDirection = Vector2.down;
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                Vector3 pos = _triggersPositions[0].position;
                _pushTrigger.transform.position = pos;
                _pushTrigger.transform.rotation = rot;
                _pullTrigger.transform.position = pos;
                _pullTrigger.transform.rotation = rot;
            }
        }
        else if (y < 0)
        {
            if (playerDirection != PlayerDirection.Down)
            {
                playerDirection = PlayerDirection.Down;
                _pusher.forceDirection = Vector2.down;
                _puller.forceDirection = Vector2.up;
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                Vector3 pos = _triggersPositions[2].position;
                _pushTrigger.transform.position = pos;
                _pushTrigger.transform.rotation = rot;
                _pullTrigger.transform.position = pos;
                _pullTrigger.transform.rotation = rot;
            }
        }
    }

    private void OnPushPerform()
    {
        if (unlockedPush && !_pushTrigger.activeSelf)
        {
            _pushTrigger.SetActive(true);
            pushing = true;
        }
    }

    private void OnPushCancel()
    {
        if (unlockedPush)
        {
            _pushTrigger.SetActive(false);
            pushing = false;
        }
    }

    private void OnPullPerform()
    {
        if (unlockedPull && !_pullTrigger.activeSelf)
        {
            _pullTrigger.SetActive(true);
            pulling = true;
        }
    }

    private void OnPullCancel()
    {
        if (unlockedPull)
        {
            _pullTrigger.SetActive(false);
            pulling = false;
        }
    }
}
