using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("Setup")]
    public GameObject playerMesh;
    public Transform orientation;
    public Animator animator;
    public Material flashMaterial;

    private new SkinnedMeshRenderer renderer;

    [Header("Attributes")]
    public float runSpeed = 5f;
    public float sprintSpeed = 10f;
    public float dashForce = 10f;
    public float dashDuration = 1.5f;
    public float dashCooldown = 2f;
    public float rotationSpeed = 10f;

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
    [NonSerialized] public readonly StateMachine<Player_Input> combatStateMachine = new();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        renderer = playerMesh.GetComponent<SkinnedMeshRenderer>();

        movementStateMachine.Initialize(new Player_Input(movementStateMachine, this), new Player_Idle());
        combatStateMachine.Initialize(new Player_Input(combatStateMachine, this), new Player_Idle());
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
        StartCoroutine(ITakeDamage(dmg));
    }
    #endregion

    #region Movement
    private float elapsedTime;
    public void CheckPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        elapsedTime += Time.deltaTime;

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
    }

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

        animator.SetBool("isMoving", true);
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

    #region Coroutines
    private IEnumerator ITakeDamage(int dmg)
    {
        base.TakeDamage(dmg);

        Material mat = renderer.material;

        renderer.material = flashMaterial;

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
