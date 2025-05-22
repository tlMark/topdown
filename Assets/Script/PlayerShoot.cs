using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab; // 총알 프리팹

    [SerializeField] private float _bulletSpeed; // 총알 속도

    [SerializeField] private Transform _gunOffset; // 총알이 생성될 위치(총구)

    [SerializeField] private float _timeBetweenShots; // 연사 간격(초)

    private bool _fireContinuously; // 연속 발사 여부
    private bool _fireSingle;       // 단발 발사 여부
    private float _lastFireTime;    // 마지막 발사 시각

    void Update()
    {
        // 발사 입력이 들어왔을 때
        if (_fireContinuously || _fireSingle)
        {
            // 마지막 발사 이후 경과 시간 계산
            float timeSinceLastFire = Time.time - _lastFireTime;

            // 연사 간격이 지났으면 총알 발사
            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet(); // 총알 생성 및 발사

                _lastFireTime = Time.time; // 마지막 발사 시각 갱신
                _fireSingle = false;       // 단발 발사 플래그 초기화
            }
        }
    }

    // 총알을 생성하고 발사하는 함수
    private void FireBullet()
    {
        // 총알 프리팹을 총구 위치에 생성
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        // 총알에 속도 부여(플레이어의 앞 방향으로)
        rigidbody.linearVelocity = _bulletSpeed * transform.up;
    }

    // 발사 입력을 처리하는 함수 (Input System 이벤트에 연결)
    private void OnFire(InputValue inputValue)
    {
        _fireContinuously = inputValue.isPressed; // 버튼이 눌려있는지 저장

        if(inputValue.isPressed)
        {
            _fireSingle = true; // 버튼이 눌릴 때 단발 발사 플래그 설정
        }
    }
}
