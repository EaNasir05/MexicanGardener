using UnityEngine;

public enum WorldDirections { Up, Down, Left, Right }

public class Wind : MonoBehaviour
{
    [SerializeField] private float forceStrength;
    [SerializeField] private WorldDirections windForceDirection;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private Vector2 boxSize = new Vector2(2f, 5f);
    private float boxAngle = 0f;
    private Vector2 forceDirection;

    private void Awake()
    {
        switch (windForceDirection)
        {
            case WorldDirections.Up:
                forceDirection = Vector2.up;
                break;
            case WorldDirections.Down:
                forceDirection = Vector2.down;
                break;
            case WorldDirections.Left:
                forceDirection = Vector2.left;
                break;
            case WorldDirections.Right:
                forceDirection = Vector2.right;
                break;
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, boxSize, transform.eulerAngles.z + boxAngle, targetLayers);

        Collider2D closest = null;
        float minDistance = float.MaxValue;
        
        foreach (var col in hitColliders)
        {
            float distance = Vector2.Distance(transform.position, col.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = col;
            }
        }
        if (closest != null)
        {
            ApplyLogic(closest);
        }
    }

    private void ApplyLogic(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        switch (other.tag)
        {
            case "Object":
                if (rb != null)
                {
                    rb.AddForce(forceDirection * forceStrength, ForceMode2D.Force);
                }
                break;
            case "LightObject":
                if (rb != null)
                {
                    rb.AddForce(forceDirection * forceStrength, ForceMode2D.Force);
                }
                break;
            case "Ghost":
                if (rb != null)
                {
                    rb.AddForce(forceDirection * forceStrength, ForceMode2D.Force);
                }
                break;
            case "Spider":
                if (rb != null)
                {
                    rb.AddForce(forceDirection * forceStrength, ForceMode2D.Force);
                }
                break;
            case "Valve":
                if (other.TryGetComponent<Valve>(out var valve))
                    valve.Rotate(-1);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Matrix4x4 combinedMatrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + boxAngle), Vector3.one);
        Gizmos.matrix = combinedMatrix;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
