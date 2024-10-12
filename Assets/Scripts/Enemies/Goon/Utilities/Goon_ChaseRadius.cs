using UnityEngine;

public class Goon_ChaseRadius : MonoBehaviour
{
    private Goon goon;

    private void Start()
    {
        goon = GetComponentInParent<Goon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var _))
        {
            goon.stateMachine.SetState(new Goon_Attacking());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var _))
        {
            goon.stateMachine.SetState(new Goon_Chasing());
        }
    }
}
