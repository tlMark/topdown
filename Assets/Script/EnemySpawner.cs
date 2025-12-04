using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Boss Settings")]
    [SerializeField] private bool _isBossSpawner = false;

    [SerializeField] private float _minimumSpawnTime;

    [SerializeField] private float _maximumSpawnTime;

    private float _timeUntilSpawn;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            GameObject enemyInstance = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            if (_isBossSpawner) 
            {
                HealthController healthController = enemyInstance.GetComponent<HealthController>();
                GameManager gameManager = FindFirstObjectByType<GameManager>();
                if (healthController != null && gameManager != null)
                {
                    healthController.OnDied.AddListener(gameManager.OnBossDied);
                }
                enabled = false;
            }
            SetTimeUntilSpawn();
        }
    }
    
    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
