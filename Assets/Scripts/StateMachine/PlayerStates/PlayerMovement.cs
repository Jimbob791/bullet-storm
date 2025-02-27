using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdle : State
{
    public override void OnEnter(Controller controller)
    {
        stateName = "PlayerIdle";

        base.OnEnter(controller);

        rb.gravityScale = 4f;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        rb.linearVelocityX = 0;
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();

        if (controller.input.GetDirectionalInput() != 0)
        {
            controller.SetNextState(new PlayerRun());
        }
        if (controller.input.GetJumpInput(true))
        {
            controller.SetNextState(new PlayerJump());
        }
        if (!controller.GetComponent<GroundedCheck>().grounded)
        {
            controller.SetNextState(new PlayerJumpFall());
        }
    }
}

public class PlayerRun : MovementState
{
    public override void OnEnter(Controller controller)
    {
        stateName = "PlayerRun";

        base.OnEnter(controller);

        anim.SetFloat("run", 1);
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();

        if (controller.input.GetDirectionalInput() == 0)
        {
            controller.SetNextState(new PlayerIdle());
        }

        if (controller.input.GetJumpInput(true))
        {
            controller.SetNextState(new PlayerJump());
        }

        if (!controller.GetComponent<GroundedCheck>().grounded)
        {
            controller.SetNextState(new PlayerJumpFall());
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        anim.SetFloat("run", 0);
    }
}

public class PlayerJump : MovementState
{
    public override void OnEnter(Controller controller)
    {
        stateName = "PlayerJump";

        base.OnEnter(controller);

        anim.SetBool("jumping", true);
        rb.linearVelocityY = controller.GetComponent<PlayerStats>().jumpStrength;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!controller.input.GetJumpInput(false))
        {
            rb.linearVelocityY *= 0.96f;
        }
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();

        if (rb.linearVelocityY < 0)
        {
            controller.SetNextState(new PlayerJumpFall());
        }
    }

    public override void OnExit()
    {
        anim.SetBool("jumping", false);
    }
}

public class PlayerJumpFall : MovementState
{
    public override void OnEnter(Controller controller)
    {
        stateName = "PlayerJumpFall";

        base.OnEnter(controller);

        rb.gravityScale = 6f;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        anim.SetBool("falling", true);
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();

        if (controller.GetComponent<GroundedCheck>().grounded)
        {
            controller.SetNextState(new PlayerIdle());
        }
    }

    public override void OnExit()
    {
        anim.SetBool("falling", false);
    }
}