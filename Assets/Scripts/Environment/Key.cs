using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private int index;
    private KeysManager _manager;

    private void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<KeysManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger colpito da: " + collision.gameObject.name + " layer: " + collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _manager.AddKey(index);
            Destroy(gameObject);
        }
    }
}
