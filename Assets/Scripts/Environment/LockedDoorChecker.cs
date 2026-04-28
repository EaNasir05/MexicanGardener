using UnityEngine;
using MoreMountains.TopDownEngine;

public class LockedDoorChecker : MonoBehaviour
{
    [SerializeField] private DungeonDoor _door; // <-- cambiato
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
            _door.OpenDoor(); // <-- cambiato
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