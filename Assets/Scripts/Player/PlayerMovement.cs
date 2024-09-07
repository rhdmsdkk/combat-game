using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float runSpeed;
    public float walkSpeed;

    private float movementSpeed;
    private bool isWalking;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readytoJump;

    [Header("Animation")]
    public Animator animator;

    [Header("Keys")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode walkToggleKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readytoJump = true;

        isWalking = false;
        movementSpeed = runSpeed;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround);

        MyInput();
        SpeedControl();

        CheckForMovementAnimation();
        CheckForMovementSpeed();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void CheckForMovementAnimation()
    {
        if (rb.velocity == Vector3.zero)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        animator.SetBool("isMoving", true);
    }

    private void CheckForMovementSpeed()
    {
        if (Input.GetKeyDown(walkToggleKey))
        {
            isWalking = !isWalking;
        }

        if (isWalking)
        {
            movementSpeed = walkSpeed;
        }
        else
        {
            movementSpeed = runSpeed;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(jumpKey) && readytoJump && grounded)
        {
            readytoJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(10f * movementSpeed * movementDirection.normalized, ForceMode.Force);
        }
        else
        {
            rb.AddForce(10f * airMultiplier * movementSpeed * movementDirection.normalized, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readytoJump = true;
    }
}
