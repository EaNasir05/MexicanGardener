using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float forceStrength;
    public Vector2 forceDirection = Vector2.up;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        switch (other.tag)
        {
            case "Object":
                if (rb != null)
                {
                    Vector2 worldForce = forceDirection;
                    rb.AddForce(worldForce * forceStrength, ForceMode.Acceleration);
                }
                break;
            case "LightObject":
                if (rb != null)
                {
                    Vector2 worldForce = forceDirection;
                    rb.AddForce(worldForce * forceStrength, ForceMode.Acceleration);
                }
                break;
            case "Ghost":
                if (rb != null)
                {
                    Vector2 worldForce = forceDirection;
                    rb.AddForce(worldForce * forceStrength, ForceMode.Acceleration);
                }
                break;
            case "Spider":
                if (rb != null)
                {
                    Vector2 worldForce = forceDirection;
                    rb.AddForce(worldForce * forceStrength, ForceMode.Acceleration);
                }
                break;
        }
    }
}
