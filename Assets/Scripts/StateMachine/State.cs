using UnityEngine;

public class State
{
    public float scaledTime;
    public float fixedTime;

    protected Controller controller;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected Transform transform;

    protected string stateName;

    public virtual void OnEnter(Controller controller)
    {
        //Debug.Log("Entered state " + stateName);
        scaledTime = 0;
        fixedTime = 0;
        this.controller = controller;
        transform = controller.transform;
        rb = controller.GetComponent<Rigidbody2D>();
        anim = controller.GetComponentInChildren<Animator>();
    }

    public virtual void OnUpdate()
    {
        scaledTime += Time.deltaTime;
        CheckTransitions();
    }

    public virtual void OnFixedUpdate()
    {
        fixedTime += Time.fixedDeltaTime;
    }

    public virtual void OnLateUpdate()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void CheckTransitions()
    {
    }
}

public class MovementState : State
{
    public override void OnUpdate()
    {
        base.OnUpdate();

        float xInput = controller.input.GetDirectionalInput();
        rb.linearVelocityX = xInput * controller.GetComponent<PlayerStats>().runSpeed;

        // Animate
        if (xInput != 0) { controller.facing = xInput; }
    }
}