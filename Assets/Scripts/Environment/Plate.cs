using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private PlatesManager _manager;
    private int objectsOnThePlate = 0;
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsOnThePlate++;
        if (!activated)
        {
            activated = true;
            _manager.IncreasePlatesActivated();
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
    }
}
