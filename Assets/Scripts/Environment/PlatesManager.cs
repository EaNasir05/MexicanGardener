using UnityEngine;
using MoreMountains.TopDownEngine;
using System.Collections.Generic;

public enum ActivatableObjectType { Door, Key, SpriteChange }

public class PlatesManager : MonoBehaviour
{
    [SerializeField] private ActivatableObjectType _objectToActivateType;
    [SerializeField] private List<GameObject> _objectsToActivate;
    [SerializeField] private int _platesNumberRequired;
    [SerializeField] private Sprite _activatedSprite;
    [SerializeField] private Sprite _deactivatedSprite;
    private int platesActivated;

    public void IncreasePlatesActivated()
    {
        platesActivated++;
        Debug.Log("IncreasePlatesActivated chiamato, platesActivated: " + platesActivated + " required: " + _platesNumberRequired);
        if (platesActivated != _platesNumberRequired) return;
        foreach (GameObject obj in _objectsToActivate)
        {
            if (obj == null) continue;
            Debug.Log("Attivando: " + obj.name);
            switch (_objectToActivateType)
            {
                case ActivatableObjectType.Door:
                    obj.GetComponent<DungeonDoor>().OpenDoor();
                    break;
                case ActivatableObjectType.Key:
                    obj.SetActive(true);
                    break;
                case ActivatableObjectType.SpriteChange:
                    var srIncrease = obj.GetComponent<SpriteRenderer>();
                    if (srIncrease.sprite != _activatedSprite)
                        srIncrease.sprite = _activatedSprite;
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
                        var srDecrease = obj.GetComponent<SpriteRenderer>();
                        if (srDecrease.sprite != _deactivatedSprite) // <-- fix
                            srDecrease.sprite = _deactivatedSprite;
                        break;
                }
            }
        }
        platesActivated--;
    }
}
