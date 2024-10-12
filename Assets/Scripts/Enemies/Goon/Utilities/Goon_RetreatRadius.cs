using UnityEngine;

public class Goon_RetreatRadius : MonoBehaviour
{
    private Goon goon;

    private void Start()
    {
        goon = GetComponentInParent<Goon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            goon.stateMachine.SetState(new Goon_Retreating());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            goon.stateMachine.SetState(new Goon_Attacking());
        }
    }
}
