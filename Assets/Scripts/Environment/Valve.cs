using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField] private PlatesManager _manager;
    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;
    [SerializeField] private float rotationSpeed;
    private float currentRot = 0;
    private bool active = false;

    public void Rotate(float multiplier)
    {
        float rot = Time.deltaTime * rotationSpeed * multiplier;
        currentRot = Mathf.Clamp(currentRot + rot, minRotation, maxRotation);
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRot));
        if (currentRot >= maxRotation && !active)
        {
            active = true;
            _manager.IncreasePlatesActivated();
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, maxRotation));
        }
        else if (currentRot <= minRotation && active)
        {
            active = false;
            _manager.DecreasePlatesActivated();
        }
    }
}
