using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private PlatesManager _manager;
    private int objectsOnThePlate = 0;
    private bool activated = false;
    private Transform CubeEntered;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (CubeEntered != null && Vector2.Distance(CubeEntered.position, transform.position)<0.1f)
        {
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
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
        }
        
        if (other.CompareTag("Object"))
        {
            CubeEntered = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        objectsOnThePlate--;
        if (objectsOnThePlate <= 0)
        {
            activated = false;
            _manager.DecreasePlatesActivated();
        }
       
    if (other.CompareTag("Object"))
        {
            CubeEntered = null;
        }
    }
}
