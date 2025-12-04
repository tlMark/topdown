using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab; // 총알 프리팹

    [SerializeField] private float _bulletSpeed; // 총알 속도

    [SerializeField] private Transform _gunOffset; // 총알이 생성될 위치(총구)

    [SerializeField] private float _timeBetweenShots; // 연사 간격(초)

    [SerializeField] private float _shotgunSpreadAngle = 10f;

    private bool _fireContinuously; // 연속 발사 여부
    private bool _fireSingle;       // 단발 발사 여부
    private float _lastFireTime;    // 마지막 발사 시각
    private bool _isShotgunActive = false;
    private float _shotgunTimer = 0f;

    void Update()
    {
        if (_isShotgunActive)
        {
            _shotgunTimer -= Time.deltaTime;
            if (_shotgunTimer <= 0)
            {
                DeactivateShotgunPowerUp();
            }
        }
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

    public void ActivateShotgunPowerUp(float duration)
    {
        _isShotgunActive = true;
        _shotgunTimer = duration;
    }

    private void DeactivateShotgunPowerUp()
    {
        _isShotgunActive = false;
        _shotgunTimer = 0f;
    }

    // 총알을 생성하고 발사하는 함수
    private void FireBullet()
    {
        if (_isShotgunActive)
        {
            FireShotgunBullets(); // 샷건 파워업 상태일 때 샷건 발사
        }

        else
        {
        // 일반 상태일 때 단일 총알 발사 (기존 로직)
            FireSingleBullet(transform.rotation, Vector3.one); // 스케일은 1배
        }
    }

    private void FireShotgunBullets()
    {
        // 샷건 총알 1: 왼쪽으로 각도 벌림
        Quaternion rotation1 = transform.rotation * Quaternion.Euler(0, 0, _shotgunSpreadAngle);
        FireSingleBullet(rotation1, Vector3.one * 2f); // 2배 스케일

        // 샷건 총알 2: 오른쪽으로 각도 벌림
        Quaternion rotation2 = transform.rotation * Quaternion.Euler(0, 0, -_shotgunSpreadAngle);
        FireSingleBullet(rotation2, Vector3.one * 2f); // 2배 스케일
    }

// [신규] 단일 총알을 발사하는 공통 로직 (분리)
    private void FireSingleBullet(Quaternion rotation, Vector3 scale)
    {
        // 총알 프리팹을 총구 위치에 생성
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, rotation);
        
        // 스케일 적용 (2배 커짐)
        bullet.transform.localScale = scale;

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        // 총알에 속도 부여 (회전된 방향으로)
        rigidbody.linearVelocity = _bulletSpeed * (rotation * Vector3.up);
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
