using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _screenBorder;

    private Rigidbody2D _rigidbody;
    private Camera _camera;
    private Animator _animator;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
        SetAnimation();
    }

    private void SetAnimation()
    {
        bool isMoving = _movementInput != Vector2.zero;

        _animator.SetBool("isMoving", isMoving);
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothVelocity, 0.1f);

        _rigidbody.linearVelocity = _movementInput * _speed;

        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        if (_camera == null)
        {
            return;
        }

        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < _screenBorder && _rigidbody.linearVelocityX < 0) ||
        (screenPosition.x > _camera.pixelWidth - _screenBorder && _rigidbody.linearVelocityX > 0))
        {
            _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocityY);
        }
        
        if ((screenPosition.y < _screenBorder && _rigidbody.linearVelocityY < 0) ||
        (screenPosition.y > _camera.pixelHeight - _screenBorder && _rigidbody.linearVelocityY > 0))
        {   
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocityX, 0);
        }
    }

    private void RotateInDirectionOfInput()
    {
        Vector3 mouseScreenPosition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;

        Vector3 direction = mouseWorldPosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(rotation);
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
}
