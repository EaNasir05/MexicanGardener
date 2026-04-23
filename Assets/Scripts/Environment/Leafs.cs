using UnityEngine;

public class Leafs : MonoBehaviour
{
    [SerializeField] private float _pullDurationRequired;
    private float totalDamage = 0;
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
            Destroy(gameObject);
    }

}
