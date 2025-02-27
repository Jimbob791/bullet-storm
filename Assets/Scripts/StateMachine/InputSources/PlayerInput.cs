using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : InputSource
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction blockAction;
    private InputAction dashAction;
    private InputAction shootAction;

    public PlayerInput()
    {
        // Find all related actions
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        blockAction = InputSystem.actions.FindAction("Block");
        dashAction = InputSystem.actions.FindAction("Dash");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    public override float GetDirectionalInput()
    {
        return moveAction.ReadValue<float>();
    }

    public override bool GetJumpInput(bool thisFrame)
    {
        if (thisFrame) { return jumpAction.WasPressedThisFrame(); }

        return jumpAction.IsPressed();
    }

    public override bool GetBlockInput()
    {
        return blockAction.IsPressed();
    }

    public override bool GetDashInput()
    {
        return dashAction.WasPressedThisFrame();
    }

    public override bool GetShootInput()
    {
        return shootAction.IsPressed();
    }
}
