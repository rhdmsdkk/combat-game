using UnityEngine;

public class Ability : MonoBehaviour
{
    public virtual void DoAbility(Entity entity)
    {
        Debug.Log("did ability");
    }
}
