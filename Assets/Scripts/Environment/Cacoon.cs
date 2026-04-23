using UnityEngine;

public class Cacoon : MonoBehaviour
{
    public float returnSpeed = 5f;
    [SerializeField] private Transform targetPosition1;
    [SerializeField] private Transform targetPosition2;
    private Vector3 originalPosition;
    private bool isInsideTriggerThisFrame;
    private Rigidbody2D _rb;
    private LineRenderer _lineRenderer;

    void Start()
    {
        originalPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.2f;
        _lineRenderer.endWidth = 0.2f;
        _lineRenderer.sortingOrder = -2;
    }

    void FixedUpdate()
    {
        _lineRenderer.SetPosition(0, originalPosition);
        _lineRenderer.SetPosition(1, transform.position);

        if (!isInsideTriggerThisFrame)
        {
            ReturnToOrigin();
        }

        isInsideTriggerThisFrame = false;
    }

    void ReturnToOrigin()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            originalPosition,
            returnSpeed * Time.fixedDeltaTime
        );
    }

    public Vector2 GetTargetPosition(Vector2 player)
    {
        if (Vector2.Distance(player, targetPosition1.position) > Vector2.Distance(player, targetPosition2.position))
            return targetPosition1.position;
        return targetPosition2.position;
    }

    public void ApplyDragForce(Vector2 force, ForceMode2D mode)
    {
        isInsideTriggerThisFrame = true;
        _rb.AddForce(force, mode);
    }
}
