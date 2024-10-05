using UnityEngine;

public class ShootWeapon : Weapon
{
    public override void DoPrimary()
    {
        Debug.Log("shoot primary");
    }

    public override void DoSecondary()
    {
        Debug.Log("shoot secondary");
    }
}
