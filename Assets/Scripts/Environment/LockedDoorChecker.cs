using UnityEngine;

public class LockedDoorChecker : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private int[] _requiredKeys;
    private KeysManager _keysManager;

    private void Start()
    {
        _keysManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<KeysManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && HasAllRequiredKeys())
        {
            _door.Open();
            Destroy(gameObject);
        }
    }

    private bool HasAllRequiredKeys()
    {
        foreach (int key in _requiredKeys)
        {
            if (!_keysManager.HasKey(key))
                return false;
        }
        return true;
    }
}
