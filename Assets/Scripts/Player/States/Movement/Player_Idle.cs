using UnityEngine;

public class Player_Idle : State<Player_Input>
{
    private Player player;

    public override void Enter(Player_Input input)
    {
        player = input.player;

        Debug.Log("idle");
    }

    public override void Update(Player_Input input)
    {
        player.CheckPlayerInput();

        if (player.horizontalInput != 0 || player.verticalInput != 0)
        {
            player.movementStateMachine.SetState(new Player_Run());
        }
    }

    public override void Exit(Player_Input input)
    {
        // stuff
    }
}
