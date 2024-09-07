using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State State { get; private set; }
    public Entity Entity { get; private set; }

    public void Initialize(Entity entity, State initState)
    {
        Entity = entity;
        State = initState;
    }

    public void SetState(State newState)
    {
        State?.Exit(Entity);

        State = newState;
        State.Enter(Entity);
    }

    public void Update()
    {
        State?.Update(Entity);
    }
}
