using UnityEngine;

public abstract class InputSource
{
    public virtual float GetDirectionalInput()
    {
        return 0;
    }

    public virtual bool GetJumpInput(bool thisFrame)
    {
        return false;
    }
    public virtual bool GetAttackInput()
    {
        return false;
    }

    public virtual bool GetBlockInput()
    {
        return false;
    }

    public virtual bool GetDashInput()
    {
        return false;
    }

    public virtual bool GetShootInput()
    {
        return false;
    }

}