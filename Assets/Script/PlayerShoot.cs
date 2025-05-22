using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _bulletSpeed;

    private bool _attackContinuously;

    void Update()
    {
        if (_attackContinuously)
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.linearVelocity = _bulletSpeed * transform.up;
    }

    private void OnFire(InputValue inputValue)
    {
        _attackContinuously = inputValue.isPressed;
    }
}
