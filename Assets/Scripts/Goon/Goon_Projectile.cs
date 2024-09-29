using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goon_Projectile : MonoBehaviour
{
    public float projectileSpeed;

    private readonly float projectileLifespan = 6f;
    private float elapsedTime = 0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * projectileSpeed;
    }

    private void Update()
    {
        CheckAutoBreak();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    private void CheckAutoBreak()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= projectileLifespan)
        {
            Break();
        }
    }

    private void Break()
    {
        Destroy(gameObject);
    }
}
