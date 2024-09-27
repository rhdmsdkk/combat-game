using UnityEngine;

public class Player_Dash : State<Player_Input>
{
    private Player player;
    private float elapsedTime = 0;
    private float dashDuration;

    public override void Enter(Player_Input input)
    {
        player = input.player;

        dashDuration = player.dashDuration;

        player.animator.SetBool("isDashing", true);

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
        player.animator.SetBool("isDashing", false);
    }

    private void Dash()
    {
        if (player.movementDirection.magnitude > 0.1)
        {
            player.rb.AddForce(player.movementDirection * player.dashForce, ForceMode.Impulse);
        }
        else
        {
            dashDuration *= 2f;
            player.rb.AddForce(player.transform.forward * player.dashForce, ForceMode.Impulse);
        }
    }
}
