using UnityEngine;

public class Goon : Enemy
{
    [Header("Setup")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject projectile;
    [SerializeField] private HealthBar healthBar;

    private new MeshRenderer renderer;

    [Header("Attributes")]
    public float moveSpeed;
    [SerializeField] private float shootTime;

    private float elapsedTime = 0f;
    private float nextShootTime;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();

        healthBar.SetHealth(health);

        nextShootTime = shootTime;
    }

    void Update()
    {
        TrackPlayer();

        Shoot();
    }

    private void TrackPlayer()
    {
        // look
        Vector3 directionToPlayer = player.position - transform.position;

        directionToPlayer.y = 0f;

        transform.forward = directionToPlayer.normalized;

        // aim
        Vector3 aimDirectionToPlayer = player.position + new Vector3(0f, 1f, 0f) - firePoint.position;

        firePoint.rotation = Quaternion.LookRotation(aimDirectionToPlayer);
    }

    private void Shoot()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= nextShootTime)
        {
            elapsedTime = 0f;
            Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
        }

        nextShootTime = shootTime + UnityEngine.Random.Range(-shootTime * (1f / 3f), shootTime * (1f / 3f));
    }

    protected override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);

        healthBar.SetHealth(health);
    }

    private void UpdateColor()
    {
        if (entityColor == EntityColor.Red)
        {
            renderer.material = new Material(colorData.redMat);
        }
        else if (entityColor == EntityColor.Blue)
        {
            renderer.material = new Material(colorData.blueMat);
        }
        else if (entityColor == EntityColor.Yellow)
        {
            renderer.material = new Material(colorData.yellowMat);
        }
        else if (entityColor == EntityColor.White)
        {
            renderer.material = new Material(colorData.whiteMat);
        }
    }

    public override void ColorEntity(EntityColor color)
    {
        base.ColorEntity(color);

        UpdateColor();
    }
}
