using UnityEngine;
using System.Collections.Generic;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _collectablePrefabs;

    public void SpawnCollectable(Vector3 position)
    {
        int index = Random.Range(0, _collectablePrefabs.Count);
        var selectedCollatable = _collectablePrefabs[index];

        Instantiate(selectedCollatable, position, Quaternion.identity);
    }
}
