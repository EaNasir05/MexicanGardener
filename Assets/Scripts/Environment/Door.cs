using UnityEngine;
using UnityEngine.WSA;

public class Door : MonoBehaviour
{
    public bool opened;
    private BoxCollider _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        if (opened)
            _collider.enabled = false;
        else
            _collider.enabled = true;
    }

    public void Open()
    {
        opened = true;
        _collider.enabled = false;
    }

    public void Close()
    {
        opened = false;
        _collider.enabled = true;
    }
}
