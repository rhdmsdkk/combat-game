using UnityEngine;

public class Goon_RetreatRadius : MonoBehaviour
{
    private Goon goon;
    private CharacterController goonController;

    private void Start()
    {
        goon = GetComponentInParent<Goon>();
        goonController = goon.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            MoveGoon(true, player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            MoveGoon(false, player);
        }
    }

    private void MoveGoon(bool isRetreating, Player player)
    {
        Debug.Log("moving");

        Vector3 directionToPlayer = player.transform.position - goon.transform.position;

        directionToPlayer.y = 0;

        float direction = isRetreating ? -1f : 1f;

        Vector3 moveDirection = direction * goon.moveSpeed * Time.deltaTime * directionToPlayer.normalized;

        goonController.Move(moveDirection);
    }
}
