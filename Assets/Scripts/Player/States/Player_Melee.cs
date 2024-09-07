using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : State
{
    public override void Enter(Entity entity)
    {
        Debug.Log("melee");
    }

    public override void Update(Entity entity)
    {
        // stuff
    }

    public override void Exit(Entity entity)
    {
        // stuff
    }
}
