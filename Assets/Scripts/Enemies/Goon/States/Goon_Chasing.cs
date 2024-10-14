using UnityEngine;
using UnityEngine.AI;

public class Goon_Chasing : State<Goon_Input>
{
    private NavMeshAgent navMeshAgent;

    public override void Enter(Goon_Input input)
    {
        Debug.Log("chasing");

        navMeshAgent = input.goon.GetComponent<NavMeshAgent>();
    }

    public override void Update(Goon_Input input)
    {
        input.goon.TrackPlayer();

        navMeshAgent.SetDestination(ChaseDirection(input));
    }

    private Vector3 ChaseDirection(Goon_Input input)
    {
        Vector3 chaseDirection = input.player.transform.position - input.goon.transform.position;

        Vector3 chaseTarget = input.goon.transform.position + chaseDirection.normalized * input.goon.chaseSpeed;

        chaseTarget.y = input.goon.transform.position.y;

        return chaseTarget;
    }

    public override void FixedUpdate(Goon_Input input) { }

    public override void Exit(Goon_Input input) { }
}
