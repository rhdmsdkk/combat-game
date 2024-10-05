using UnityEngine;

public class Goon : Enemy
{
    [Header("Setup")]
    public Transform firePoint;
    public Transform player;
    public GameObject projectile;

    private new MeshRenderer renderer;

    [Header("Materials")]
    public Material redMat;
    public Material blueMat;
    public Material yellowMat;

    [Header("Attributes")]
    public float shootTime;

    private float elapsedTime = 0f;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= shootTime)
        {
            elapsedTime = 0f;
            Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
        }
    }

    protected override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }

    private void UpdateColor()
    {
        if (entityColor == EntityColor.Red)
        {
            renderer.material = new Material(redMat);
        }
        else if (entityColor == EntityColor.Blue)
        {
            renderer.material = new Material(blueMat);
        }
        else if (entityColor == EntityColor.Yellow)
        {
            renderer.material = new Material(yellowMat);
        }
    }

    public override void ColorEntity(EntityColor color)
    {
        base.ColorEntity(color);

        UpdateColor();
    }
}
