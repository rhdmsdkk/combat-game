using UnityEngine;

public class Goon_Attacking : State<Goon_Input>
{
    public override void Enter(Goon_Input input)
    {
        Debug.Log("attacking");
    }

    public override void Update(Goon_Input input)
    {
        input.goon.TrackPlayer();

        input.goon.Shoot();
    }

    public override void FixedUpdate(Goon_Input input) { }

    public override void Exit(Goon_Input input) { }
}
