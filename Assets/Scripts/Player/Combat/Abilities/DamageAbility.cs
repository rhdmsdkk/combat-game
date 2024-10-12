using UnityEngine;

public class DamageAbility : Ability
{
    public override void DoAbility(Player player, Entity target)
    {
        Instantiate(player.abilityData.explodeParticleSystem, target.transform.position, Quaternion.identity);
        target.Stagger(player.abilityData.explodeStaggerAmount);
        target.TakeDamage(player.abilityData.explodeDamage);
    }
}
