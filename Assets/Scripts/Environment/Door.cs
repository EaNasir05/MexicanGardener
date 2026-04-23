using UnityEngine;
using UnityEngine.WSA;

public class Door : MonoBehaviour
{
    public bool opened;
    private Sprite _closedDoorSprite;
    [SerializeField] private Sprite _openedDoorSprite;
    [SerializeField] private GameObject _locker;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _closedDoorSprite = _spriteRenderer.sprite;
        _collider = GetComponent<BoxCollider2D>();
        if (opened)
            _collider.enabled = false;
        else
            _collider.enabled = true;
    }

    public void Open()
    {
        opened = true;
        if (_locker != null)
            _locker.SetActive(false);
        _collider.enabled = false;
        _spriteRenderer.sprite = _openedDoorSprite;
    }

    public void Close()
    {
        opened = false;
        _collider.enabled = true;
        _spriteRenderer.sprite = _closedDoorSprite;
    }
}
