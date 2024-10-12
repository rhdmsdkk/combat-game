using UnityEngine;

public class Goon_Retreating : State<Goon_Input>
{
    public override void Enter(Goon_Input input)
    {
        Debug.Log("retreating");

        input.goon.controller.enabled = true;
    }

    public override void Update(Goon_Input input)
    {
        input.goon.TrackPlayer();

        input.goon.controller.Move(RetreatDirection(input));
    }

    private Vector3 RetreatDirection(Goon_Input input)
    {
        Vector3 retreatDirection = (input.goon.transform.position - input.player.transform.position).normalized * input.goon.retreatSpeed * Time.deltaTime;

        retreatDirection.y = 0f;

        return retreatDirection;
    }

    public override void FixedUpdate(Goon_Input input) { }

    public override void Exit(Goon_Input input) { }
}
