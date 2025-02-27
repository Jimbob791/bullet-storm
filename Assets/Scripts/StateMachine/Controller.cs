using UnityEngine;

public class Controller : MonoBehaviour
{
    public State currentState;

    private State nextState;
    protected State defaultState;

    public InputSource input;

    public float facing = 1;

    protected virtual void Start()
    {
        SetState(defaultState);
    }

    protected virtual void Update()
    {
        currentState.OnUpdate();

        if (nextState != null)
        {
            SetState(nextState);
            nextState = null;
        }

        // Flip animation
        transform.localScale = new Vector3(facing, 1, 1);
    }

    protected virtual void FixedUpdate()
    {
        currentState.OnFixedUpdate();
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }

    public void SetDefaultState()
    {
        SetNextState(defaultState);
    }

    public void SetState(State state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = state;
        currentState.OnEnter(this);
    }
}
