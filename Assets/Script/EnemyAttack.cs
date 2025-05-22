using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _dmamageAmount;

    private void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();
            
            healthController.TakeDamage(_dmamageAmount);
        }
    }
}
