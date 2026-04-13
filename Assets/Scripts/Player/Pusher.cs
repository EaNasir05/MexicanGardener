using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float forceStrength;
    public Vector2 forceDirection = Vector2.up;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Object") || other.CompareTag("Spider") || other.CompareTag("Ghost") || other.CompareTag("LightObject"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector2 worldForce = transform.TransformDirection(forceDirection);
                rb.AddForce(worldForce * forceStrength, ForceMode.Acceleration);
            }
        }
    }
}
