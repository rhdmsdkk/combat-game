using System;
using UnityEngine;

public class Player : Entity
{
    [Header("Setup")]
    [SerializeField] private GameObject playerMesh;
    [SerializeField] public Transform orientation;

    [Header("Attributes")]
    [SerializeField] private int playerHealth;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;

    [Header("Inputs")]
    [NonSerialized] public float horizontalInput = 0;
    [NonSerialized] public float verticalInput = 0;
    [NonSerialized] public bool isDashing = false;

    [NonSerialized] public float movementSpeed = 5f;
    [NonSerialized] public Vector3 movementDirection;

    [NonSerialized] public Rigidbody rb;

    [NonSerialized] public readonly StateMachine<Player_Input> movementStateMachine = new();
    [NonSerialized] public readonly StateMachine<Player_Input> combatStateMachine = new();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        health = playerHealth;

        movementStateMachine.Initialize(new Player_Input(movementStateMachine, this), new Player_Idle());
        combatStateMachine.Initialize(new Player_Input(combatStateMachine, this), new Player_Idle());
    }

    private void Update()
    {
        movementStateMachine.Update();
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
    }

    public void Move()
    {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        movementDirection.y = 0;

        rb.velocity = movementDirection * movementSpeed + new Vector3(0, rb.velocity.y, 0);
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
