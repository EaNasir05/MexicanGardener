using UnityEngine;

public class PullPickup : MonoBehaviour
{
    [SerializeField] private GameObject _spriteToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.unlockedPull = true;
                player.ableToPull = true;
                if (_spriteToActivate != null)
                    _spriteToActivate.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
