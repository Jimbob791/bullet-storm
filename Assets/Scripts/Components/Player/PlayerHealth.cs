using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] IconBar healthBar;
    public GameObject hurtSFX;
    [SerializeField] float shakeStrength;
    [SerializeField] float shakeDuration;
    public float regenDelay = 0;
    public override void UpdateVisuals()
    {
        healthBar.UpdateBar(Mathf.RoundToInt(maxHealth), Mathf.RoundToInt(currentHealth));
    }

    float t;

    public override void Damage(float amount)
    {
        if (currentHealth - amount <= 0)
        {
            StartCoroutine(PlayerDeath());
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            return;
        }
        Instantiate(hurtSFX);

        base.Damage(amount);

        
        CameraShake.instance.ShakeCamera(shakeDuration, shakeStrength);
    }

    private IEnumerator PlayerDeath()
    {
        HelperFunctions.ModifyTimeForDuration(0.5f, 2f);

        yield return new WaitForSeconds(1f);
        EnemySpawnManager manager = (EnemySpawnManager)FindFirstObjectByType(typeof(EnemySpawnManager));
        manager.Lost();
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= regenDelay && regenDelay != 0 && currentHealth < maxHealth)
        {
            Heal(1);
            regenDelay = 0;
        }
    }
}
