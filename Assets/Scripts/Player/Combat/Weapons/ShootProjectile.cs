using UnityEngine;

public class ShootProjectile : Projectile
{
    public EntityColor projectileColor;

    [Header("Materials")]
    public Material redMat;
    public Material blueMat;
    public Material yellowMat;

    protected override void DoImpact(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.ColorEntity(projectileColor);
            // enemy.TakeDamage(1);
        }

        base.DoImpact(other);
    }

    public void SetColor(EntityColor color)
    {
        projectileColor = color;

        if (projectileColor == EntityColor.Red)
        {
            GetComponent<MeshRenderer>().material = redMat;
        }
        else if (projectileColor == EntityColor.Blue)
        {
            GetComponent<MeshRenderer>().material = blueMat;
        }
        else if (projectileColor == EntityColor.Yellow)
        {
            GetComponent<MeshRenderer>().material = yellowMat;
        }
    }
}
