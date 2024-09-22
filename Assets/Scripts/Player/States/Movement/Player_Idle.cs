using UnityEngine;

public class Player_Idle : State<Player_Input>
{
    private Player player;
    private Vector3 lastTransform;
    private Quaternion lastRotation;

    public override void Enter(Player_Input input)
    {
        player = input.player;

        lastTransform = player.transform.position;

        lastRotation = player.transform.rotation;
    }

    public override void Update(Player_Input input)
    {
        player.CheckPlayerInput();

        CheckSlope();

        if (player.horizontalInput != 0 || player.verticalInput != 0)
        {
            player.movementStateMachine.SetState(new Player_Run());
        }

        if (player.shouldDash)
        {
            player.movementStateMachine.SetState(new Player_Dash());
        }
    }

    public override void FixedUpdate(Player_Input input)
    {
    }

    public override void Exit(Player_Input input)
    {
        // stuff
    }

    private void CheckSlope()
    {
        if (Physics.Raycast(player.transform.position, Vector3.down, out RaycastHit slopeHit, 1.5f))
        {
            Vector3 surfaceNormal = slopeHit.normal;

            float slopeAngle = Vector3.Angle(surfaceNormal, Vector3.up);

            if (slopeAngle > 0)
            {
                player.transform.position = lastTransform;
            }
        }
    }
}
