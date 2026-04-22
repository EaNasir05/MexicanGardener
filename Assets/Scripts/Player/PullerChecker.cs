using System.Collections;
using UnityEngine;

public class PullerChecker : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 pushDirection;
    [SerializeField] private float shotStrength;
    [SerializeField] private int _ghostLayer = 27;
    private int _playerLayer;
    private PlayerController _playerController;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _playerLayer = _player.layer;
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
            case "Cacoon":
                StartCoroutine(JumpWithCacoon(collision.GetComponent<Cacoon>().GetTargetPosition(_player.transform.position)));
                break;
        }
    }

    private IEnumerator JumpWithCacoon(Vector2 target)
    {
        _player.transform.position = target;
        return null;
        /*
        _player.layer = _ghostLayer;
        _playerController.StopPulling();
        _playerController.ableToPull = false;
        _playerController.ableToPush = false;
        Vector2 direction = ((Vector2)_player.transform.position - target).normalized;
        _player.GetComponent<Rigidbody2D>().linearVelocity = direction * 15;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, target) > 0.15);
        _player.layer = _playerLayer;
        _playerController.ableToPull = true;
        _playerController.ableToPush = true;
        */
    }

    public void Shoot()
    {
        Pot pot = projectile.GetComponent<Pot>();
        pot.stuck = false;
        pot.dangerous = true;
        projectile.GetComponent<Rigidbody2D>().AddForce(pushDirection * shotStrength, ForceMode2D.Impulse);
        projectile = null;
    }
}
