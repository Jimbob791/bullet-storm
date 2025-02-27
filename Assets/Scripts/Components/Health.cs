using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    [Header("Visuals")]
    [SerializeField] float flashTime;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Slider slider;
    [SerializeField] bool useSlider = false;
    [SerializeField] protected GameObject deathEffect;
    [SerializeField] protected GameObject deathSFX;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateVisuals();
    }

    public virtual void Damage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Damaged for " + amount + "damage. " + currentHealth + "/" + maxHealth);
        if (currentHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(deathSFX);
            CameraShake.instance.StartCoroutine(HelperFunctions.ModifyTimeForDuration(0f, 0.2f));
            CameraShake.instance.ShakeCamera(0.3f, 0.3f);
            Destroy(gameObject);
        }

        StartCoroutine(DamageFlash(flashTime));

        UpdateVisuals();
    }

    private IEnumerator DamageFlash(float duration)
    {
        sr.material.SetFloat("_hit", 1);
        yield return new WaitForSeconds(duration);
        sr.material.SetFloat("_hit", 0);
    }

    public virtual void Heal(float amount)
    {
        currentHealth += amount;
        UpdateVisuals();
    }

    public virtual void UpdateVisuals()
    {
        if (useSlider)
        {
            slider.maxValue = maxHealth;
            slider.value = currentHealth;
        }
    }
}