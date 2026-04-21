using MoreMountains.TopDownEngine;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public bool stuck = false;
    public bool dangerous = false;
    public Transform puller;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (stuck)
            transform.position = puller.position;
        if (dangerous && _rb.linearVelocity.magnitude < 0)
            dangerous = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dangerous)
        {
            if (collision.transform.CompareTag("Spider"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                dangerous = false;
            }
        }
    }
}
