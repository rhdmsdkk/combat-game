using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Setup")]
    [SerializeField] private GameObject playerMesh;

    [Header("Attributes")]
    [SerializeField] private int playerHealth;

    private readonly StateMachine stateMachine = new();

    private void Awake()
    {
        health = playerHealth;
        stateMachine.Initialize(this, new MeleeState());
    }

    private void Update()
    {
        stateMachine.Update();
        SelectWeapon();
    }

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

    private void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            stateMachine.SetState(new MeleeState());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stateMachine.SetState(new RangedState());
        }
    }
}
