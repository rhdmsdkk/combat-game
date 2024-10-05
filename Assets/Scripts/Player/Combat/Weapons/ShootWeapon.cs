using UnityEngine;
public class ShootWeapon : Weapon
{
    public float fireRate = 0.2f;
    public Transform firePoint;
    public GameObject projectile;

    private float lastShootTime;

    public override void DoPrimary()
    {
        if (Time.time - lastShootTime > fireRate)
        {
            Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
            
            if (projectile.TryGetComponent<ShootProjectile>(out var proj))
            {
                proj.projectileColor = weaponColor;
            }

            lastShootTime = Time.time;
        }
    }

    public override void DoSecondary()
    {
        Debug.Log(weaponColor + " shoot secondary");
    }
}