using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private PlatesManager _manager;
    [SerializeField] private GameObject _wind; // <-- aggiunto
    private int objectsOnThePlate = 0;
    private bool activated = false;
    private Transform CubeEntered;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (CubeEntered != null && Vector2.Distance(CubeEntered.position, transform.position) < 0.1f)
        {
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            CubeEntered = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsOnThePlate++;
        if (!activated)
        {
            activated = true;
            _manager.IncreasePlatesActivated();
            if (_wind != null)
                _wind.SetActive(false); 
        }

        if (other.CompareTag("Object"))
            CubeEntered = other.transform;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        objectsOnThePlate--;
        if (objectsOnThePlate <= 0)
        {
            activated = false;
            _manager.DecreasePlatesActivated();
            if (_wind != null)
                _wind.SetActive(true); 
        }

        if (other.CompareTag("Object"))
            CubeEntered = null;
    }
}
