using UnityEngine;

public class HealAbility : Ability
{
    public override void DoAbility(Entity entity)
    {
        Debug.Log("healed");
    }
}
