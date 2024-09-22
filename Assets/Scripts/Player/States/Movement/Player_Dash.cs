using UnityEngine;

public class Player_Dash : State<Player_Input>
{
    private Player player;
    private float elapsedTime = 0;

    public override void Enter(Player_Input input)
    {
        player = input.player;

        Dash();
    }

    public override void Update(Player_Input input)
    {
        player.CheckPlayerInput();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= player.dashDuration)
        {
            if (player.isDashing || player.wasSprinting)
            {
                player.movementStateMachine.SetState(new Player_Sprint());
            }
            else
            {
                player.movementStateMachine.SetState(new Player_Run());
            }
        }
    }

    public override void FixedUpdate(Player_Input input)
    {
    }

    public override void Exit(Player_Input input)
    {
    }

    private void Dash()
    {
        if (player.movementDirection.magnitude > 0.1)
        {
            player.rb.AddForce(player.movementDirection * player.dashForce, ForceMode.Impulse);
        }
        else
        {
            player.rb.AddForce(player.transform.forward * player.dashForce, ForceMode.Impulse);
        }
    }
}
