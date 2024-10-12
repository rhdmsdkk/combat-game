using UnityEngine;

public class Goon_Chasing : State<Goon_Input>
{
    public override void Enter(Goon_Input input)
    {
        Debug.Log("chasing");

        input.goon.controller.enabled = true;
    }

    public override void Update(Goon_Input input)
    {
        input.goon.TrackPlayer();

        input.goon.controller.Move(ChaseDirection(input));
    }

    private Vector3 ChaseDirection(Goon_Input input)
    {
        Vector3 chaseDirection = (input.player.transform.position - input.goon.transform.position).normalized * input.goon.chaseSpeed * Time.deltaTime;

        chaseDirection.y = 0f;

        return chaseDirection;
    }

    public override void FixedUpdate(Goon_Input input) { }

    public override void Exit(Goon_Input input) { }
}
