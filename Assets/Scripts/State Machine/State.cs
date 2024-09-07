using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter(Entity entity);
    public abstract void Update(Entity entity);
    public abstract void Exit(Entity entity);
}
