using UnityEngine;

public class EnemyColletableDrop : MonoBehaviour
{
    [SerializeField] private float _chanceOfColletableDrop;

    private CollectableSpawner _collectableSpawner;

    private void Awake()
    {
        _collectableSpawner = FindFirstObjectByType<CollectableSpawner>();
    }


    public void RandomlyDropCollectable()
    {
        float random = Random.Range(0f, 1f);
        if (_chanceOfColletableDrop >= random)
        {
            _collectableSpawner.SpawnCollectable(transform.position);
        }
    }
}
