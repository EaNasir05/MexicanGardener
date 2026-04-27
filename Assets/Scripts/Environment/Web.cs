using UnityEngine;
using MoreMountains.TopDownEngine;
using System.Collections.Generic;

public class Web : MonoBehaviour
{
    [SerializeField] private float _pullDurationRequired;
    [SerializeField] private List<DungeonDoor> _doors;
    [SerializeField] private Web _linkedWeb;
    public float totalDamage = 0;
    private Vector3 startingScale;
    private Vector3 endingScale;

    private void Awake()
    {
        startingScale = transform.localScale;
        endingScale = new Vector3(0.1f, 0.1f, 1);
    }

    public void RemoveLeafs(float damage)
    {
        totalDamage += damage;
        Vector3 scale = Vector3.Lerp(startingScale, endingScale, totalDamage / _pullDurationRequired);
        transform.localScale = scale;
        if (totalDamage >= _pullDurationRequired)
        {
            OpenAllDoors();

            if (_linkedWeb != null)
                _linkedWeb.DestroyWeb();

            Destroy(gameObject);
        }
    }

    public void DestroyWeb()
    {
        OpenAllDoors();
        Destroy(gameObject);
    }

    private void OpenAllDoors()
    {
        foreach (DungeonDoor door in _doors)
        {
            if (door != null)
                door.OpenDoor();
        }
    }
}
