using UnityEngine;

public enum PlateObjectType { Door }

public class Plate : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private PlateObjectType objectType;
    private int objectsOnThePlate = 0;
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsOnThePlate++;
        if (!activated)
        {
            activated = true;
            switch (objectType)
            {
                case PlateObjectType.Door:
                    objectToActivate.GetComponent<Door>().Open();
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        objectsOnThePlate--;
        if (objectsOnThePlate <= 0)
        {
            activated = false;
            switch (objectType)
            {
                case PlateObjectType.Door:
                    objectToActivate.GetComponent<Door>().Close();
                    break;
            }
        }
    }
}
