using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField] private PlatesManager _manager;
    [SerializeField] private PlatesManager _alternativeManager;
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
            _manager.IncreasePlatesActivated();
        }
        else if (active)
        {
            active = false;
            _manager.DecreasePlatesActivated();
        }
        if (currentRot <= minRotation && !altActive)
        {
            altActive = true;
            if (_alternativeManager != null)
                _alternativeManager.IncreasePlatesActivated();
        }
        else if (altActive)
        {
            altActive = false;
            if (_alternativeManager != null)
                _alternativeManager.DecreasePlatesActivated();
        }
    }
}
