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
    public Transform thirdPersonCamera;
    public HealthBar healthBar;

    [NonSerialized] public Ability[] abilities = new Ability[3];

    private new SkinnedMeshRenderer renderer;

    [Header("Attributes")]
    public float runSpeed = 5f;
    public float sprintSpeed = 10f;
    public float dashForce = 10f;
    public float dashDuration = 1.5f;
    public float dashCooldown = 2f;
    public float rotationSpeed = 10f;

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
    [NonSerialized] public WeaponType currentWeaponType;

    [NonSerialized] public readonly StateMachine<Player_Input> movementStateMachine = new();

    private void Awake()
    {
        entityColor = EntityColor.Red;

        abilities[0] = gameObject.AddComponent<HealAbility>();
        abilities[1] = gameObject.AddComponent<DamageAbility>();

        rb = GetComponent<Rigidbody>();
        currentWeaponType = weaponHolder.transform.GetChild(weaponHolder.currentWeapon).GetComponent<Weapon>().weaponType;

        renderer = playerMesh.GetComponent<SkinnedMeshRenderer>();

        movementStateMachine.Initialize(new Player_Input(movementStateMachine, this), new Player_Idle());

        healthBar.SetHealth(health);
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

        // attacks
        if (Input.GetMouseButtonDown(0))
        {
            PerformPrimary();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            PerformSecondary();
            CheckWeaponType();
        }
    }
    #endregion

    #region Movement
    public void Move()
    {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        movementDirection.y = 0;

        rb.velocity = movementDirection * movementSpeed + new Vector3(0, rb.velocity.y, 0);

        if (movementDirection != Vector3.zero && currentWeaponType == WeaponType.Basic)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else if (currentWeaponType == WeaponType.Aimed)
        {
            Vector3 cameraForward = new(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);

            Quaternion targetRotation = Quaternion.LookRotation(cameraForward.normalized);
            transform.rotation = targetRotation;
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
            renderer.material = new Material(colorData.redMat);

            weaponHolder.SelectWeapon(0);
        }
        else if (entityColor == EntityColor.Blue)
        {
            renderer.material = new Material(colorData.blueMat);

            weaponHolder.SelectWeapon(1);
        }
        else if (entityColor == EntityColor.Yellow)
        {
            renderer.material = new Material(colorData.yellowMat);

            weaponHolder.SelectWeapon(2);
        }

        CheckWeaponType();
    }

    public void CheckWeaponType()
    {
        currentWeaponType = weaponHolder.transform.GetChild(weaponHolder.currentWeapon).GetComponent<Weapon>().weaponType;
    }

    void PerformPrimary()
    {
        Transform currentWeaponTransform = GetCurrentWeapon();
        if (currentWeaponTransform != null)
        {
            if (currentWeaponTransform.TryGetComponent<Weapon>(out var currentWeapon)) currentWeapon.DoPrimary();
        }
    }
    void PerformSecondary()
    {
        Transform currentWeaponTransform = GetCurrentWeapon();
        if (currentWeaponTransform != null)
        {
            if (currentWeaponTransform.TryGetComponent<Weapon>(out var currentWeapon)) currentWeapon.DoSecondary();
        }
    }
    Transform GetCurrentWeapon()
    {
        foreach (Transform weapon in weaponHolder.transform)
        {
            if (weapon.gameObject.activeSelf)
            {
                return weapon;
            }
        }
        return null;
    }
    #endregion

    #region Coroutines
    private IEnumerator ITakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        healthBar.SetHealth(health);

        renderer.material = new Material(colorData.flashMaterial);

        yield return new WaitForSeconds(0.2f);

        if (entityColor == EntityColor.Red)
        {
            renderer.material = colorData.redMat;
        }
        else if (entityColor == EntityColor.Blue)
        {
            renderer.material = colorData.blueMat;
        }
        else
        {
            renderer.material = colorData.yellowMat;
        }
    }

    private IEnumerator IDie()
    {
        yield return new WaitForSeconds(0.25f);

        movementStateMachine.SetState(new Player_Dead());
    }
    #endregion
}
