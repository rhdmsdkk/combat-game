using System;
using System.Collections;
using UnityEngine;

/*
 * to add player to a scene:
 *     + UI Canvas
 *     + Camera - assign all orientation to orientation/follow/lookAt
 *              - Canvas/Crosshair -> crosshair
 *     + Player - UI Canvas/Health Bar -> healthBar
 *     
 * OR:
 *     + Player Package
 */

public class Player : Entity
{
    [Header("Setup")]
    public HealthBar healthBar;
    public SkinnedMeshRenderer playerRenderer;
    public PlayerData playerData;
    public Transform abilityHolder;

    [NonSerialized] public GeneralAbilityData abilityData;
    [NonSerialized] public SwitchWeapon weaponHolder;
    [NonSerialized] public Animator animator;
    [NonSerialized] public Transform thirdPersonCamera;
    [NonSerialized] public Ability[] abilities = new Ability[3];

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
    [NonSerialized] public WeaponType currentWeaponType;

    [NonSerialized] public readonly StateMachine<Player_Input> movementStateMachine = new();

    private void Awake()
    {
        InitializeData();
    }

    private void Update()
    {
        movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
        movementStateMachine.FixedUpdate();
    }

    #region Initialize
    public void InitializeData()
    {
        entityColor = EntityColor.Red;

        abilityData = playerData.abilityData;

        animator = GetComponentInChildren<Animator>();

        weaponHolder = GetComponentInChildren<SwitchWeapon>();
        weaponHolder.currentWeapon = 0;
        weaponHolder.Initialize();

        SetAbilities();
        SetWeapons();

        currentWeaponType = weaponHolder.transform.GetChild(weaponHolder.currentWeapon).GetComponent<Weapon>().weaponType;

        thirdPersonCamera = FindAnyObjectByType<ThirdPersonCamera>().transform;

        rb = GetComponent<Rigidbody>();

        movementStateMachine.Initialize(new Player_Input(movementStateMachine, this), new Player_Idle());

        playerData.maxHealth = health;
        healthBar.SetHealth(health);
    }

    public void SetAbilities()
    {
        foreach (Transform child in abilityHolder)
        {
            Destroy(child.gameObject);
        }

        Ability redAb;
        Ability blueAb;
        Ability yellowAb;

        // add red ability
        if (playerData.redAbility != null && playerData.redAbility.ability != null)
        {
            redAb = Instantiate(playerData.redAbility.ability, abilityHolder).GetComponent<Ability>();
        }
        else
        {
            redAb = Instantiate(playerData.blankAbility.ability, abilityHolder).GetComponent<Ability>();
        }

        // add blue ability
        if (playerData.blueAbility != null && playerData.blueAbility.ability != null)
        {
            blueAb = Instantiate(playerData.blueAbility.ability, abilityHolder).GetComponent<Ability>();
        }
        else
        {
            blueAb = Instantiate(playerData.blankAbility.ability, abilityHolder).GetComponent<Ability>();
        }

        // add yellow ability
        if (playerData.yellowAbility != null && playerData.yellowAbility.ability != null)
        {
            yellowAb = Instantiate(playerData.yellowAbility.ability, abilityHolder).GetComponent<Ability>();
        }
        else
        {
            yellowAb = Instantiate(playerData.blankAbility.ability, abilityHolder).GetComponent<Ability>();
        }

        abilities[0] = redAb;
        abilities[1] = blueAb;
        abilities[2] = yellowAb;
    }

    private void SetWeapons()
    {
        foreach (Transform child in weaponHolder.transform)
        {
            Destroy(child.gameObject);
        }

        Weapon redWeap;
        Weapon blueWeap;
        Weapon yellowWeap;

        // add red weapon
        if (playerData.redWeapon != null && playerData.redWeapon.weapon != null)
        {
            redWeap = Instantiate(playerData.redWeapon.weapon, weaponHolder.transform).GetComponent<Weapon>();
        }
        else
        {
            redWeap = Instantiate(playerData.blankWeapon.weapon, weaponHolder.transform).GetComponent<Weapon>();
        }

        // add blue weapon
        if (playerData.blueWeapon != null && playerData.blueWeapon.weapon != null)
        {
            blueWeap = Instantiate(playerData.blueWeapon.weapon, weaponHolder.transform).GetComponent<Weapon>();
        }
        else
        {
            blueWeap = Instantiate(playerData.blankWeapon.weapon, weaponHolder.transform).GetComponent<Weapon>();
        }

        // add yellow weapon
        if (playerData.yellowWeapon != null && playerData.yellowWeapon.weapon != null)
        {
            yellowWeap = Instantiate(playerData.yellowWeapon.weapon, weaponHolder.transform).GetComponent<Weapon>();
        }
        else
        {
            yellowWeap = Instantiate(playerData.blankWeapon.weapon, weaponHolder.transform).GetComponent<Weapon>();
        }

        redWeap.weaponColor = EntityColor.Red;
        blueWeap.weaponColor = EntityColor.Blue;
        yellowWeap.weaponColor = EntityColor.Yellow;

        weaponHolder.Initialize();
    }
    #endregion

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

    public override void Heal(int hp)
    {
        if (health + hp > playerData.maxHealth)
        {
            health = playerData.maxHealth;
            healthBar.SetHealth(health);
            return;
        }
        base.Heal(hp);
        healthBar.SetHealth(health);
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
        movementDirection = thirdPersonCamera.forward * verticalInput + thirdPersonCamera.right * horizontalInput;

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
            playerRenderer.material = new Material(colorData.redMat);

            weaponHolder.SelectWeapon(0);
        }
        else if (entityColor == EntityColor.Blue)
        {
            playerRenderer.material = new Material(colorData.blueMat);

            weaponHolder.SelectWeapon(1);
        }
        else if (entityColor == EntityColor.Yellow)
        {
            playerRenderer.material = new Material(colorData.yellowMat);

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

        playerRenderer.material = new Material(colorData.flashMaterial);

        yield return new WaitForSeconds(0.2f);

        if (entityColor == EntityColor.Red)
        {
            playerRenderer.material = colorData.redMat;
        }
        else if (entityColor == EntityColor.Blue)
        {
            playerRenderer.material = colorData.blueMat;
        }
        else
        {
            playerRenderer.material = colorData.yellowMat;
        }
    }

    private IEnumerator IDie()
    {
        yield return new WaitForSeconds(0.25f);

        movementStateMachine.SetState(new Player_Dead());
    }
    #endregion
}
