using UnityEngine;
public class ShootWeapon : Weapon
{
    public float fireRate = 0.2f;
    public Transform firePoint;

    private float lastShootTime;
    private Transform aimCamera;

    public override void DoPrimary()
    {
        if (Time.time - lastShootTime > fireRate)
        {
            Ray ray = new(aimCamera.position, aimCamera.forward);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.ColorEntity(weaponColor);
            }

            lastShootTime = Time.time;
        }
    }

    public override void DoSecondary()
    {
        if (weaponType == WeaponType.Aimed)
        {
            weaponType = WeaponType.Basic;
        }
        else
        {
            weaponType = WeaponType.Aimed;
        }
    }

    private void Start()
    {
        aimCamera = FindAnyObjectByType<Camera>().transform;
    }
}