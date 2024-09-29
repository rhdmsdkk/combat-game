using UnityEngine;

public class Goon : Entity
{
    [Header("Setup")]
    public Transform firePoint;
    public Transform player;
    public GameObject projectile;

    private new MeshRenderer renderer;

    [Header("Materials")]
    public Material whiteMat;
    public Material redMat;
    public Material blueMat;
    public Material yellowMat;

    [Header("Attributes")]
    public float shootTime;

    private float elapsedTime = 0f;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();

        entityColor = EntityColor.White;
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
