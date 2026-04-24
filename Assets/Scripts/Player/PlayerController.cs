using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum PlayerDirection { Left, Right, Up, Down }

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite[] characterSprites;
    
    [SerializeField] private float pullForceStrength;
    [SerializeField] private float pushForceStrength;
    public bool unlockedPull;
    public bool unlockedPush;
    public bool ableToPull = true;
    public bool ableToPush = true;
    public bool ableToMove = true;

    [SerializeField] private GameObject _pushTrigger;
    [SerializeField] private GameObject _pullTrigger;
    [SerializeField] private Transform[] _triggersPositions;
    private Puller _puller;
    private Pusher _pusher;
    private InputHandler _inputHandler;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private PullerChecker _pullerChecker;

    private bool movingHorizontally = false;
    private bool movingVertically = false;
    private bool pulling = false;
    private bool pushing = false;
    private PlayerDirection playerDirection;

    public bool IsPulling() => pulling;
    public bool IsPushing() => pushing;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
    }

    private void UpdatePlayerDirection(Vector2 input)
    {
        float x = input.x;
        float y = input.y;
        if (x > 0)
        {
            if (playerDirection != PlayerDirection.Right && !pushing && !pulling)
            {
                playerDirection = PlayerDirection.Right;
                _pusher.forceDirection = Vector2.right;
                _pullerChecker.pushDirection = Vector2.right;
                _puller.forceDirection = Vector2.left;
                Quaternion rot = Quaternion.Euler(0, 0, 90);
                Vector3 pos = _triggersPositions[1].position;
                _pushTrigger.transform.position = pos;
                _pushTrigger.transform.rotation = rot;
                _pullTrigger.transform.position = pos;
                _pullTrigger.transform.rotation = rot;
                //_spriteRenderer.sprite = characterSprites[1];
            }
        }
        else if (x < 0)
        {
            if (playerDirection != PlayerDirection.Left && !pushing && !pulling)
            {
                playerDirection = PlayerDirection.Left;
                _pusher.forceDirection = Vector2.left;
                _pullerChecker.pushDirection = Vector2.left;
                _puller.forceDirection = Vector2.right;
                Quaternion rot = Quaternion.Euler(0, 0, 90);
                Vector3 pos = _triggersPositions[3].position;
                _pushTrigger.transform.position = pos;
                _pushTrigger.transform.rotation = rot;
                _pullTrigger.transform.position = pos;
                _pullTrigger.transform.rotation = rot;
                //_spriteRenderer.sprite = characterSprites[3];
            }
        }
        else if (y > 0)
        {
            if (playerDirection != PlayerDirection.Up && !pushing && !pulling)
            {
                playerDirection = PlayerDirection.Up;
                _pusher.forceDirection = Vector2.up;
                _pullerChecker.pushDirection = Vector2.up;
                _puller.forceDirection = Vector2.down;
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                Vector3 pos = _triggersPositions[0].position;
                _pushTrigger.transform.position = pos;
                _pushTrigger.transform.rotation = rot;
                _pullTrigger.transform.position = pos;
                _pullTrigger.transform.rotation = rot;
                //_spriteRenderer.sprite = characterSprites[0];
            }
        }
        else if (y < 0)
        {
            if (playerDirection != PlayerDirection.Down && !pushing && !pulling)
            {
                playerDirection = PlayerDirection.Down;
                _pusher.forceDirection = Vector2.down;
                _pullerChecker.pushDirection = Vector2.down;
                _puller.forceDirection = Vector2.up;
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                Vector3 pos = _triggersPositions[2].position;
                _pushTrigger.transform.position = pos;
                _pushTrigger.transform.rotation = rot;
                _pullTrigger.transform.position = pos;
                _pullTrigger.transform.rotation = rot;
                //_spriteRenderer.sprite = characterSprites[2];
            }
        }
    }

    private void OnPushPerform()
    {
        if (unlockedPush && !_pushTrigger.activeSelf && ableToPush)
        {
            if (_pullerChecker.projectile != null)
                _pullerChecker.Shoot();
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
        if (unlockedPull && !_pullTrigger.activeSelf && _pullerChecker.projectile == null && ableToPull)
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

    public void StopPulling()
    {
        _pullTrigger.SetActive(false);
        pulling = false;
    }
}
