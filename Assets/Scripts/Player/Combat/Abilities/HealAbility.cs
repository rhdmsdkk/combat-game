public class HealAbility : Ability
{
    public override void DoAbility(Player player, Entity target)
    {
        player.Heal(player.abilityData.healAmount);
    }
}
