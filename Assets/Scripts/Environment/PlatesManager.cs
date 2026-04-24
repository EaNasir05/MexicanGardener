using UnityEngine;

public enum ActivatableObjectType { Door, Key }

public class PlatesManager : MonoBehaviour
{
    [SerializeField] private ActivatableObjectType _objectToActivateType;
    [SerializeField] private GameObject _objectToActivate;
    [SerializeField] private int _platesNumberRequired;
    private int platesActivated;

    public void IncreasePlatesActivated()
    {
        platesActivated++;
        switch (_objectToActivateType)
        {
            case ActivatableObjectType.Door:
                if (platesActivated == _platesNumberRequired)
                    _objectToActivate.GetComponent<Door>().Open();
                break;
            case ActivatableObjectType.Key:
                if (platesActivated == _platesNumberRequired)
                    _objectToActivate.SetActive(true);
                break;
        }
    }

    public void DecreasePlatesActivated()
    {
        switch (_objectToActivateType)
        {
            case ActivatableObjectType.Door:
                if (platesActivated == _platesNumberRequired)
                    _objectToActivate.GetComponent<Door>().Close();
                break;
        }
        platesActivated--;
    }
}
