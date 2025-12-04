using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField] private float _playerAwarenessDistance;

    private Transform _player;

    [System.Obsolete]
    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (_player == null)
        {
            AwareOfPlayer = false; // 선택 사항: 인지 상태를 비활성화
            return; 
        }

        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
