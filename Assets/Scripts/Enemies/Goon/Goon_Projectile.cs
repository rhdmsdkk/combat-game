using UnityEngine;

public class Goon_Projectile : Projectile
{
    protected override void DoImpact(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(1);
        }

        base.DoImpact(other);
    }
}
