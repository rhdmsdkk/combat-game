using System.Collections;
using UnityEngine;
public class ShootWeapon : Weapon
{
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private ParticleSystem impactParticleSystem;
    [SerializeField] private float bulletSpeed = 100f;

    private float lastShootTime;
    private Transform aimCamera;

    private bool canShoot = false;

    public override void DoPrimary()
    {
        if (canShoot && Time.time - lastShootTime > fireRate)
        {
            Ray ray = new(aimCamera.position, aimCamera.forward);

            TrailRenderer trail = Instantiate(bulletTrail, firePoint.position, Quaternion.identity);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                StartCoroutine(ISpawnTrail(trail, hit, hit.point, hit.normal, true));
            }
            else
            {
                StartCoroutine(ISpawnTrail(trail, hit, firePoint.position + aimCamera.forward * 100, Vector3.zero, false));
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

        canShoot = !canShoot;
    }

    private void Start()
    {
        aimCamera = FindAnyObjectByType<Camera>().transform;
    }

    private void Update()
    {
        if (canShoot)
        {
            Vector3 cameraRotation = aimCamera.rotation.eulerAngles;

            Vector3 newRotation = new(cameraRotation.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            transform.rotation = Quaternion.Euler(newRotation);
        }
    }

    private void OnDisable()
    {
        weaponType = WeaponType.Basic;
        canShoot = false;
    }

    IEnumerator ISpawnTrail(TrailRenderer trail, RaycastHit hit, Vector3 hitPoint, Vector3 hitNormal, bool madeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, hitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= bulletSpeed * Time.deltaTime;

            yield return null;
        }

        trail.transform.position = hitPoint;

        if (madeImpact)
        {
            Instantiate(impactParticleSystem, hitPoint, Quaternion.LookRotation(hitNormal));

            if (hit.collider.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.ColorEntity(weaponColor);
                // enemy.TakeDamage(1);
            }
        }

        Destroy(trail.gameObject, trail.time);
    }
}