using UnityEngine;

public class Goon_Retreating : State<Goon_Input>
{
    private Rigidbody rb;

    public override void Enter(Goon_Input input)
    {
        Debug.Log("retreating");

        rb = input.goon.GetComponent<Rigidbody>();
    }

    public override void Update(Goon_Input input)
    {
        input.goon.TrackPlayer();
    }

    public override void FixedUpdate(Goon_Input input)
    {
        rb.velocity = RetreatDirection(input);
    }

    private Vector3 RetreatDirection(Goon_Input input)
    {
        Vector3 retreatDirection = input.goon.retreatSpeed * (input.goon.transform.position - input.player.transform.position).normalized;

        retreatDirection.y = 0f;

        return retreatDirection;
    }

    public override void Exit(Goon_Input input) { }
}
