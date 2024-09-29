using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goon : Entity
{
    [Header("Setup")]
    public Transform firePoint;
    public Transform player;
    public GameObject projectile;

    [Header("Attributes")]
    public float shootTime;

    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= shootTime)
        {
            elapsedTime = 0f;
            Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
        }
    }
}
