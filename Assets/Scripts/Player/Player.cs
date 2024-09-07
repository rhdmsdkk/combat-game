using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    protected StateMachine stateMachine = new();

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
