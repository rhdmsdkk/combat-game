using System;
using System.Collections;
using UnityEngine;

public class Goon : Enemy
{
    [Header("Setup")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ParticleSystem deathParticleSystem;

    private new MeshRenderer renderer;

    [Header("Attributes")]
    public float moveSpeed;
    public float retreatSpeed = 2f;
    public float chaseSpeed = 4f;
    [SerializeField] private float shootTime;

    [NonSerialized] public readonly StateMachine<Goon_Input> stateMachine = new();

    private float elapsedTime = 0f;
    private float nextShootTime;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();

        healthBar.SetHealth(health);

        stateMachine.Initialize(new Goon_Input(stateMachine, this, FindAnyObjectByType<Player>()), new Goon_Attacking());

        nextShootTime = shootTime;
    }

    void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void TrackPlayer()
    {
        // look
        Vector3 directionToPlayer = stateMachine.Input.player.transform.position - transform.position;

        directionToPlayer.y = 0f;

        transform.forward = directionToPlayer.normalized;

        // aim
        Vector3 aimDirectionToPlayer = stateMachine.Input.player.transform.position + new Vector3(0f, 1f, 0f) - firePoint.position;

        firePoint.rotation = Quaternion.LookRotation(aimDirectionToPlayer);
    }

    public void Shoot()
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

        Instantiate(deathParticleSystem, firePoint.position + new Vector3(0f, 0.25f, 0f), firePoint.rotation);

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

    public override void Stagger(float staggerAmount)
    {
        base.Stagger(staggerAmount);

        stateMachine.SetState(new Goon_Staggered());
    }

    [NonSerialized] public bool isStaggered = false;

    public void Wait()
    {
        StartCoroutine(IWait());
    }

    IEnumerator IWait()
    {
        isStaggered = true;

        yield return new WaitForSeconds(0.5f);

        isStaggered = false;

        stateMachine.SetState(new Goon_Attacking());
    }
}
