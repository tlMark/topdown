using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _screenBorder;

    [SerializeField] private float _obstacleCheckCircleRadius;

    [SerializeField] private float _obstacleCheckDistance;

    [SerializeField] private LayerMask _obstacleLayerMask;


    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Camera _camera;
    private Vector2 _targetDirection;
    private Vector2 _obstacleAvoidanceTargetDirection;
    private RaycastHit2D[] _obstacleCollisions;

    private float _changeDirectionCooldown;
    private float _obstacleAvoidanceCooldown;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();

        _obstacleCollisions = new RaycastHit2D[10];

        _camera = Camera.main;

        _targetDirection = transform.up;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        HandleObstacles();
        HandleEnemyOffScreen();
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
       if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
    }

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < _screenBorder && _targetDirection.x < 0) ||
        (screenPosition.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }
        
        if ((screenPosition.y < _screenBorder && _targetDirection.y < 0) ||
        (screenPosition.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0))
        {   
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    private void HandleObstacles()
    {
        _obstacleAvoidanceCooldown -= Time.deltaTime;

        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_obstacleLayerMask);

        int numberOFCollisions = Physics2D.CircleCast(transform.position, _obstacleCheckCircleRadius, transform.up, contactFilter,
        _obstacleCollisions, _obstacleCheckDistance);

        for (int i = 0; i < numberOFCollisions; i++)
        {
            var obstacleCollision = _obstacleCollisions[i];

            if (obstacleCollision.collider.gameObject == gameObject)
            {
                continue;
            }

            if (_obstacleAvoidanceCooldown <= 0)
            {
                _obstacleAvoidanceTargetDirection = obstacleCollision.normal;
                _obstacleAvoidanceCooldown = 0.5f;
            }

            var targetRotation = Quaternion.LookRotation(transform.forward, _obstacleAvoidanceTargetDirection);
            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _targetDirection = rotation * Vector2.up;
            break;
        }
    }

    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
            _rigidbody.linearVelocity = transform.up * _speed;
    }
}
