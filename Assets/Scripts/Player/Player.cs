using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("Setup")]
    public GameObject playerMesh;
    public Transform orientation;
    public Animator animator;
    public SwitchWeapon weaponHolder;
    public ThirdPersonCamera thirdPersonCamera;

    private new SkinnedMeshRenderer renderer;

    [Header("Attributes")]
    public float runSpeed = 5f;
    public float sprintSpeed = 10f;
    public float dashForce = 10f;
    public float dashDuration = 1.5f;
    public float dashCooldown = 2f;
    public float rotationSpeed = 10f;

    [Header("Materials")]
    public Material flashMat;
    public Material redMat;
    public Material blueMat;
    public Material yellowMat;

    [Header("Enemy")]
    public Goon goon;

    // inputs
    [NonSerialized] public float horizontalInput = 0;
    [NonSerialized] public float verticalInput = 0;
    [NonSerialized] public bool isDashing = false;
    [NonSerialized] public bool shouldDash = false;
    [NonSerialized] public bool wasSprinting = false;

    [NonSerialized] public float movementSpeed = 5f;
    [NonSerialized] public Vector3 movementDirection;

    [NonSerialized] public Rigidbody rb;

    [NonSerialized] public readonly StateMachine<Player_Input> movementStateMachine = new();

    private void Awake()
    {
        entityColor = EntityColor.Red;

        rb = GetComponent<Rigidbody>();
        renderer = playerMesh.GetComponent<SkinnedMeshRenderer>();

        movementStateMachine.Initialize(new Player_Input(movementStateMachine, this), new Player_Idle());
    }

    private void Update()
    {
        movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
        movementStateMachine.FixedUpdate();
    }

    #region General
    protected override void Die()
    {
        StartCoroutine(IDie());
    }

    public override void TakeDamage(int dmg)
    {
        // iframes
        if (!isDashing)
        {
            StartCoroutine(ITakeDamage(dmg));
        }
    }

    private float elapsedTime;
    public void CheckPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        elapsedTime += Time.deltaTime;

        // movement
        if (Input.GetKeyDown(KeyCode.LeftShift) && elapsedTime > dashCooldown)
        {
            elapsedTime = 0;
            shouldDash = true;
        }
        else
        {
            shouldDash = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }

        // color
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ColorEntity(EntityColor.Red);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ColorEntity(EntityColor.Blue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ColorEntity(EntityColor.Yellow);
        }

        if (Input.GetMouseButtonDown(0))
        {
            goon.ColorEntity(entityColor);
        }
    }

    #endregion

    #region Movement
    public void Move()
    {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        movementDirection.y = 0;

        rb.velocity = movementDirection * movementSpeed + new Vector3(0, rb.velocity.y, 0);

        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void SetRunSpeed()
    {
        movementSpeed = runSpeed;
    }

    public void SetSprintSpeed()
    {
        movementSpeed = sprintSpeed;
    }
    #endregion

    #region Color/Combat
    public override void ColorEntity(EntityColor color)
    {
        base.ColorEntity(color);

        UpdateColor();
    }

    private void UpdateColor()
    {
        if (entityColor == EntityColor.Red)
        {
            renderer.material = new Material(redMat);

            weaponHolder.SelectWeapon(0);

            thirdPersonCamera.style = CameraStyle.Basic;
        }
        else if (entityColor == EntityColor.Blue)
        {
            renderer.material = new Material(blueMat);

            weaponHolder.SelectWeapon(1);

            thirdPersonCamera.style = CameraStyle.Aim;
        }
        else if (entityColor == EntityColor.Yellow)
        {
            renderer.material = new Material(yellowMat);

            weaponHolder.SelectWeapon(2);

            thirdPersonCamera.style = CameraStyle.Basic;
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator ITakeDamage(int dmg)
    {
        base.TakeDamage(dmg);

        Material mat = new(renderer.material);

        renderer.material = new Material(flashMat);

        yield return new WaitForSeconds(0.2f);

        renderer.material = mat;
    }

    private IEnumerator IDie()
    {
        yield return new WaitForSeconds(0.25f);

        playerMesh.GetComponent<Renderer>().enabled = false;
        Destroy(this);
    }
    #endregion
}
