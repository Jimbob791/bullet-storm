using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBulletTime : MonoBehaviour
{
    private InputAction timeSlowAction;

    public float timeSlowScale;
    public float maxSlowSeconds;
    public float currentSlowSeconds;
    public float pointRecoveryRate = 0.5f;

    public GameObject timeSlowSFX;
    public GameObject timeUnslowSFX;

    [SerializeField] Slider slider;

    bool ranOut = false;
    bool slowing = false;

    private void Start()
    {
        timeSlowAction = InputSystem.actions.FindAction("TimeSlow");
    }

    private void Update()
    {
        if (HelperFunctions.timeChanged | HelperFunctions.tutorial) { return; }

        if (timeSlowAction.WasPressedThisFrame())
        {
            slowing = true;
            Instantiate(timeSlowSFX);
        }

        if (timeSlowAction.WasReleasedThisFrame())
        {
            slowing = false;
            Instantiate(timeUnslowSFX);
        }

        if (slowing)
        {
            if (currentSlowSeconds > 0)
            {
                Time.timeScale = timeSlowScale;
                currentSlowSeconds -= Time.deltaTime;
                currentSlowSeconds = currentSlowSeconds < 0 ? 0 : currentSlowSeconds;
                UpdateBar();
            }
            else
            {
                slowing = false;
            }
        }
        else
        {
            Time.timeScale = 1f;
            currentSlowSeconds += Time.deltaTime * pointRecoveryRate;
            currentSlowSeconds = currentSlowSeconds > maxSlowSeconds ? maxSlowSeconds : currentSlowSeconds;
            UpdateBar();
        }
    }

    private void UpdateBar()
    {
        slider.maxValue = maxSlowSeconds;
        slider.value = currentSlowSeconds;
    }
}
