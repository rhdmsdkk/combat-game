using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private GameObject playerMesh;

    private readonly StateMachine stateMachine = new();

    private void Awake()
    {
        health = 10;
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
