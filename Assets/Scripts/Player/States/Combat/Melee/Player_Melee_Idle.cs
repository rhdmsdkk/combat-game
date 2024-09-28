using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Melee_Idle : State<Player_Input>
{
    public override void Enter(Player_Input input)
    {
        input.player.ChangeOutline("melee");
    }

    public override void Update(Player_Input input)
    {
        // stuff
    }

    public override void FixedUpdate(Player_Input input)
    {
        // stuff
    }
    public override void Exit(Player_Input input)
    {
        // stuff
    }
}
