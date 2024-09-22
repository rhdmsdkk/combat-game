using System;
using UnityEngine;

public class Player : Entity
{
    [Header("Setup")]
    [SerializeField] private GameObject playerMesh;
    [SerializeField] public Transform orientation;

    [Header("Attributes")]
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] public float dashForce = 10f;
    [SerializeField] public float dashDuration = 1.5f;
    [SerializeField] public float rotationSpeed = 10f;

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
    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);

        Debug.Log("health: " + health);
    }

    protected override void Die()
    {
        Debug.Log("died");
        playerMesh.GetComponent<Renderer>().enabled = false;
    }
    #endregion

    #region Movement
    public void CheckPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
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
}
