using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float elapsedTime = 0f;
    public float projectileSpeed;

    private Rigidbody rb;
    private readonly float projectileLifespan = 6f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * projectileSpeed;
    }
    private void Update()
    {
        CheckAutoBreak();
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

    private void OnTriggerEnter(Collider other)
    {
        DoImpact(other);
    }

    protected virtual void DoImpact(Collider other)
    {
        Destroy(gameObject);
    }
}
