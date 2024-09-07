using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(1);
        }
    }
}
