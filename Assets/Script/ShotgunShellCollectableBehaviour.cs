using UnityEngine;

// ICollectableBehaviour 인터페이스를 상속받습니다.
public class ShotgunShellCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    [Header("Shotgun Power-up Settings")]
    [Tooltip("샷건 파워업이 유지될 시간(초)을 설정합니다.")]
    [SerializeField] private float _powerUpDuration = 5f; 

    // Colletable.cs에 의해 호출됩니다.
    public void OnCollected(GameObject player)
    {
        // Player의 PlayerShoot 컴포넌트를 찾습니다.
        PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();

        if (playerShoot != null)
        {
            // PlayerShoot에 파워업 지속 시간을 넘겨 활성화를 요청합니다.
            playerShoot.ActivateShotgunPowerUp(_powerUpDuration);
        }
        else
        {
            Debug.LogError("PlayerShoot component not found on the player object!");
        }
    }
}