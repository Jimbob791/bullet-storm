using UnityEngine;

public class PlayerController : Controller
{
    protected override void Start()
    {
        defaultState = new PlayerIdle();
        input = new PlayerInput();

        base.Start();
    }
}
