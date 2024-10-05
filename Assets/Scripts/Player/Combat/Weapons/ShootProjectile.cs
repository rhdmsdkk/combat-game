using UnityEngine;

public class ShootProjectile : Projectile
{
    public EntityColor projectileColor;

    protected override void DoImpact(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.ColorEntity(projectileColor);
            // enemy.TakeDamage(1);
        }

        base.DoImpact(other);
    }
}
