using UnityEngine;

public class DamageAbility : Ability
{
    public override void DoAbility(Entity entity)
    {
        Instantiate(GetComponent<Player>().abilityData.explodeParticleSystem, entity.transform.position, Quaternion.identity);
        entity.Stagger(2f);
        entity.TakeDamage(5);
    }
}
