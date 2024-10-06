using System;
using UnityEngine;

public class ShootProjectile : Projectile
{
    public EntityColor projectileColor;

    [Header("Materials")]
    public Material redMat;
    public Material blueMat;
    public Material yellowMat;

    private Transform aimCamera;

    private void Start()
    {
        aimCamera = FindAnyObjectByType<Camera>().transform;
        rb = GetComponent<Rigidbody>();

        if (aimCamera != null)
        {
            Ray ray = new(aimCamera.position, aimCamera.forward);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 aimDir = (hit.point - transform.position).normalized;

                rb.velocity = aimDir * projectileSpeed;
            }
            else
            {
                rb.velocity = aimCamera.forward * projectileSpeed;
            }
        }
    }

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
