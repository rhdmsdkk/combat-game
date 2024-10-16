using UnityEngine;

public class Player_Sprint : State<Player_Input>
{
    private Player player;

    public override void Enter(Player_Input input)
    {
        player = input.player;

        player.SetSprintSpeed();

        player.animator.SetBool("isSprinting", true);

        player.wasSprinting = true;
    }

    public override void Update(Player_Input input)
    {
        player.CheckPlayerInput();

        if (player.horizontalInput == 0 && player.verticalInput == 0)
        {
            player.movementStateMachine.SetState(new Player_Idle());
        }

        if (player.shouldDash)
        {
            player.movementStateMachine.SetState(new Player_Dash());
        }
    }

    public override void FixedUpdate(Player_Input input)
    {
        player.Move();
    }

    public override void Exit(Player_Input input)
    {
        player.animator.SetBool("isSprinting", false);
    }
}
