using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeWeapon : Weapon
{
    public override void DoPrimary()
    {
        Debug.Log("explode primary");
    }

    public override void DoSecondary()
    {
        Debug.Log("explode secondary");
    }
}
