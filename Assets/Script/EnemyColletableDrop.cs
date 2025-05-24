using UnityEngine;
using System.Collections;

public class EnemyColletableDrop : MonoBehaviour
{
    [SerializeField] private float _chanceOfColletableDrop;

    private CollectableSpawner _collectableSpawner;

    private void Awake()
    {
        _collectableSpawner = FindFirstObjectByType<CollectableSpawner>();
    }

    public void RandomlyDropCollectable(float delay)
    {
        StartCoroutine(RandomlyDropCollectableCoroutine(delay));
    }

    private IEnumerator RandomlyDropCollectableCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        float random = Random.Range(0f, 1f);

        if (_chanceOfColletableDrop >= random)
        {
            if (_collectableSpawner != null)
                _collectableSpawner.SpawnCollectable(transform.position);
        }
    }
}
