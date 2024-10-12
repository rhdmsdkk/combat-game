using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float elapsedTime = 0f;
    public float projectileSpeed;

    protected Rigidbody rb;
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

    protected void CheckAutoBreak()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= projectileLifespan)
        {
            Break();
        }
    }

    protected void Break()
    {
        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        DoImpact(other);
    }

    protected virtual void DoImpact(Collider other)
    {
        Destroy(gameObject);
    }
}
