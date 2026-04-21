using UnityEngine;

public class Puller : MonoBehaviour
{
    public float forceStrength;
    public Vector2 forceDirection = Vector2.down;

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        switch (other.tag)
        {
            case "Object":
                if (rb != null)
                {
                    Vector2 worldForce = forceDirection;
                    rb.AddForce(worldForce * forceStrength, ForceMode2D.Impulse);
                }
                break;
            case "LightObject":
                if (rb != null)
                {
                    Vector2 worldForce = forceDirection;
                    rb.AddForce(worldForce * forceStrength, ForceMode2D.Impulse);
                }
                break;
            case "Leafs":
                other.GetComponent<Leafs>().RemoveLeafs(Time.deltaTime);
                break;
            case "Web":
                other.GetComponent<Web>().RemoveLeafs(Time.deltaTime);
                break;
            case "Valve":
                other.GetComponent<Valve>().Rotate(1);
                break;
            case "Ghost":
                if (rb != null)
                {
                    Vector2 worldForce = forceDirection;
                    rb.AddForce(worldForce * forceStrength, ForceMode2D.Impulse);
                }
                break;
            case "Spider":
                if (rb != null)
                {
                    Vector2 worldForce = forceDirection;
                    rb.AddForce(worldForce * forceStrength, ForceMode2D.Impulse);
                }
                break;
        }
    }
}
