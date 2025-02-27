using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    InputAction closeAction;

    private void Start()
    {
        HelperFunctions.timeChanged = true;
        Time.timeScale = 0f;
        closeAction = InputSystem.actions.FindAction("TimeSlow");
    }

    private void Update()
    {
        if (closeAction.WasPressedThisFrame())
        {
            HelperFunctions.timeChanged = false;
            HelperFunctions.tutorial = false;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
