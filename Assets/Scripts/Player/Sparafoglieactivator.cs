using UnityEngine;

public class PullUnlockedVisual : MonoBehaviour
{
    [SerializeField] private GameObject _visualToActivate;
    private PlayerController _player;

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerController>();
        if (_visualToActivate != null)
            _visualToActivate.SetActive(false);
    }

    private void Update()
    {
        if (_player != null && _player.unlockedPull && _player.ableToPull)
        {
            if (_visualToActivate != null)
                _visualToActivate.SetActive(true);
        }
    }
}
