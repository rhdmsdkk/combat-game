using UnityEngine;

public class Player_Run : State<Player_Input>
{
    private Player player;

    public override void Enter(Player_Input input)
    {
        player = input.player;

        player.SetRunSpeed();

        player.wasSprinting = false;

        Debug.Log("run");
    }

    public override void Update(Player_Input input)
    {
        player.CheckPlayerInput();

        player.Move();

        if (player.horizontalInput == 0 && player.verticalInput == 0)
        {
            player.movementStateMachine.SetState(new Player_Idle());
        }

        if (player.shouldDash)
        {
            player.movementStateMachine.SetState(new Player_Dash());
        }
    }

    public override void Exit(Player_Input input)
    {
    }
}
