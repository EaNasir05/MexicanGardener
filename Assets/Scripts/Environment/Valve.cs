using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField] private List<PlatesManager> _managers;
    [SerializeField] private List<PlatesManager> _alternativeManagers;
    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;
    [SerializeField] private float rotationSpeed;
    private float currentRot = 0;
    private bool active = false;
    private bool altActive = false;

    public void Rotate(float multiplier)
    {
        float rot = Time.deltaTime * rotationSpeed * multiplier;
        currentRot = Mathf.Clamp(currentRot + rot, minRotation, maxRotation);
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRot));

        if (currentRot >= maxRotation && !active)
        {
            active = true;
            foreach (PlatesManager manager in _managers)
                if (manager != null) manager.IncreasePlatesActivated();
        }
        else if (currentRot < maxRotation && active)
        {
            active = false;
            foreach (PlatesManager manager in _managers)
                if (manager != null) manager.DecreasePlatesActivated();
        }

        if (currentRot <= minRotation && !altActive)
        {
            altActive = true;
            foreach (PlatesManager manager in _alternativeManagers)
                if (manager != null) manager.IncreasePlatesActivated();
        }
        else if (currentRot > minRotation && altActive)
        {
            altActive = false;
            foreach (PlatesManager manager in _alternativeManagers)
                if (manager != null) manager.DecreasePlatesActivated();
        }
    }
}
