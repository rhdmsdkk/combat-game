using UnityEngine;

public class Goon_Chasing : State<Goon_Input>
{
    private Rigidbody rb;

    public override void Enter(Goon_Input input)
    {
        Debug.Log("chasing");

        rb = input.goon.GetComponent<Rigidbody>();
    }

    public override void Update(Goon_Input input)
    {
        input.goon.TrackPlayer();
    }

    public override void FixedUpdate(Goon_Input input)
    {
        rb.velocity = ChaseDirection(input);
    }

    private Vector3 ChaseDirection(Goon_Input input)
    {
        Vector3 chaseDirection = input.goon.chaseSpeed * (input.player.transform.position - input.goon.transform.position).normalized;

        chaseDirection.y = 0f;

        return chaseDirection;
    }

    public override void Exit(Goon_Input input) { }
}
