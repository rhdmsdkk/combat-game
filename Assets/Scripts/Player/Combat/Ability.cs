using UnityEngine;

public class Ability : MonoBehaviour
{
    public virtual void DoAbility(Player player, Entity target)
    {
        Debug.Log("did ability");
    }
}
