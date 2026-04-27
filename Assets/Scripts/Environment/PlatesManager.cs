using UnityEngine;
using MoreMountains.TopDownEngine;
using System.Collections.Generic;

public enum ActivatableObjectType { Door, Key, SpriteChange }

public class PlatesManager : MonoBehaviour
{
    [SerializeField] private ActivatableObjectType _objectToActivateType;
    [SerializeField] private List<GameObject> _objectsToActivate; // <-- lista invece di singolo
    [SerializeField] private int _platesNumberRequired;
    [SerializeField] private Sprite _activatedSprite;
    [SerializeField] private Sprite _deactivatedSprite;
    private int platesActivated;

    public void IncreasePlatesActivated()
    {
        platesActivated++;
        if (platesActivated != _platesNumberRequired) return;

        foreach (GameObject obj in _objectsToActivate)
        {
            if (obj == null) continue;
            switch (_objectToActivateType)
            {
                case ActivatableObjectType.Door:
                    obj.GetComponent<DungeonDoor>().OpenDoor();
                    break;
                case ActivatableObjectType.Key:
                    obj.SetActive(true);
                    break;
                case ActivatableObjectType.SpriteChange:
                    obj.GetComponent<SpriteRenderer>().sprite = _activatedSprite;
                    break;
            }
        }
    }

    public void DecreasePlatesActivated()
    {
        if (platesActivated == _platesNumberRequired)
        {
            foreach (GameObject obj in _objectsToActivate)
            {
                if (obj == null) continue;
                switch (_objectToActivateType)
                {
                    case ActivatableObjectType.Door:
                        obj.GetComponent<DungeonDoor>().CloseDoor();
                        break;
                    case ActivatableObjectType.SpriteChange:
                        obj.GetComponent<SpriteRenderer>().sprite = _deactivatedSprite;
                        break;
                }
            }
        }
        platesActivated--;
    }
}
