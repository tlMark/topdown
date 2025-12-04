using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timeToWaitBeforeExit;
    [SerializeField] private GameObject _player;
    [SerializeField] private SceneController _sceneController;

    [Header("Spawner Management")]
    [SerializeField] private List<GameObject> _activeSpawners;
    [SerializeField] private GameObject _bossSpawner;
    [SerializeField] private float _bossPhaseStartTime = 60.0f;

    private void Start()
    {
        Invoke(nameof(StartBossPhase), _bossPhaseStartTime);
    }

    private void StartBossPhase()
    {
        Debug.Log("Boss Phase Starting!");

        foreach (var spawner in _activeSpawners)
        {
            if (spawner != null)
            {
                spawner.SetActive(false);
            }
        }

        if (_bossSpawner != null)
        {
            _bossSpawner.SetActive(true);
        }
    }

    public void OnPlayerDied()
    {
        Invoke(nameof(EndGame), _timeToWaitBeforeExit);
    }
    
    private void EndGame()
    {
        _sceneController.LoadScene("MainMenu");
    }

    public void OnBossDied()
    {
        Debug.Log("Boss Defeated! Game Clear!");
        if (_player != null)
        {
            MonoBehaviour[] scripts = _player.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
            Destroy(_player);
        }
        Destroy(gameObject);
        _sceneController.LoadScene("GameClear");
    }
}
