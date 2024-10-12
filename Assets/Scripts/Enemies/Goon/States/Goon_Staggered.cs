using UnityEngine;

public class Goon_Staggered : State<Goon_Input>
{
    public override void Enter(Goon_Input input)
    {
        Debug.Log("staggered");
        input.goon.Wait();
    }

    public override void Update(Goon_Input input) { }

    public override void FixedUpdate(Goon_Input input) { }

    public override void Exit(Goon_Input input) { }
}
