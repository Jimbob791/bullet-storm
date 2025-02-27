using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlocking : MonoBehaviour
{
    Animator anim;
    private InputAction blockAction;
    public bool blocking;
    public CircleCollider2D parryCollider;
    public float timeSinceBlocked;

    [Header("Effects")]
    [SerializeField] GameObject parryEffect;
    [SerializeField] GameObject parrySFX;
    [SerializeField] GameObject blockSFX;
    public float parryShakeStrength;
    public float parryShakeDuration;
    

    private void Start()
    {
        blockAction = InputSystem.actions.FindAction("Block");
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        blocking = blockAction.IsPressed();

        anim.SetFloat("blocking", blocking ? 1 : 0);

        if (blockAction.WasPressedThisFrame())
        {
            timeSinceBlocked = 0;
            Instantiate(blockSFX);
        }

        parryCollider.enabled = blocking;

        if (blocking)
        {
            timeSinceBlocked += Time.deltaTime;
        }
        else
        {
            timeSinceBlocked = 100f;
        }
    }

    public bool AttemptParry()
    {
        if (timeSinceBlocked <= Player.instance.stats.parryWindow)
        {
            Instantiate(parryEffect, transform.position, Quaternion.identity);
            Instantiate(parrySFX);
            StartCoroutine(HelperFunctions.ModifyTimeForDuration(0f, 0.4f));
            CameraShake.instance.ShakeCamera(parryShakeDuration, parryShakeStrength);
            return true;
        }
        else
        {
            return false;
        }
    }
}
