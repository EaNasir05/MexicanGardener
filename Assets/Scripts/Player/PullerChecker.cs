using UnityEngine;

public class PullerChecker : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private Pusher _pusher;
    [SerializeField] private float shotStrength;

    private void Awake()
    {
        _pusher = GetComponent<Pusher>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "LightObject":
                Pot pot = collision.GetComponent<Pot>();
                pot.puller = transform;
                pot.stuck = true;
                projectile = pot.gameObject;
                break;
            case "Ghost":
                Destroy(collision.gameObject);
                break;
        }
    }

    public void Shoot()
    {
        Pot pot = projectile.GetComponent<Pot>();
        pot.stuck = false;
        pot.dangerous = true;
        //NON TROVA IL PUSHER
        projectile.GetComponent<Rigidbody2D>().AddForce(_pusher.forceDirection * shotStrength, ForceMode2D.Impulse);
        projectile = null;
    }
}
