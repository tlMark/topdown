using System.Collections;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private HealthController _healthController;
    private SpriteFlash _spriteFlash;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _spriteFlash = GetComponent<SpriteFlash>();
    }

    public void StartInvincibility(float invincibilityDuration, Color flashColor, int numberOfFlashes)
    {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration, flashColor, numberOfFlashes));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration, Color flashColor, int numberOfFlashes)
    {
        _healthController.IsInvincible = true;
        yield return _spriteFlash.FlashCoroutine(invincibilityDuration, flashColor, numberOfFlashes);
        _healthController.IsInvincible = false;
    }
}
