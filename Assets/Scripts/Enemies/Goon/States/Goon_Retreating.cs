using UnityEngine;
using UnityEngine.AI;

public class Goon_Retreating : State<Goon_Input>
{
    private NavMeshAgent navMeshAgent;

    public override void Enter(Goon_Input input)
    {
        Debug.Log("retreating");

        navMeshAgent = input.goon.GetComponent<NavMeshAgent>();

        navMeshAgent.enabled = true;

        navMeshAgent.updateRotation = false;
    }

    public override void Update(Goon_Input input)
    {
        input.goon.TrackPlayer();

        navMeshAgent.SetDestination(RetreatDirection(input));
    }

    private Vector3 RetreatDirection(Goon_Input input)
    {
        Vector3 retreatDirection = input.goon.transform.position - input.player.transform.position;

        Vector3 retreatTarget = input.goon.transform.position + retreatDirection.normalized * input.goon.retreatSpeed;

        retreatTarget.y = input.goon.transform.position.y;

        return retreatTarget;
    }

    public override void FixedUpdate(Goon_Input input) { }

    public override void Exit(Goon_Input input) { }
}
