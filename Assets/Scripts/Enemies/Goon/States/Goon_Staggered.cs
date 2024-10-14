using UnityEngine;
using UnityEngine.AI;

public class Goon_Staggered : State<Goon_Input>
{
    private NavMeshAgent navMeshAgent;

    public override void Enter(Goon_Input input)
    {
        Debug.Log("staggered");

        navMeshAgent = input.goon.GetComponent<NavMeshAgent>();

        navMeshAgent.ResetPath();

        navMeshAgent.enabled = false;

        // navMesh is reenabled in coroutine
        input.goon.Wait();
    }

    public override void Exit(Goon_Input input) { }

    public override void Update(Goon_Input input) { }

    public override void FixedUpdate(Goon_Input input) { }
}
