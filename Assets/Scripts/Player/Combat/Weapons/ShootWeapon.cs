using UnityEngine;
public class ShootWeapon : Weapon
{
    public override void DoPrimary()
    {
        Debug.Log(weaponColor + " shoot primary");
    }
    public override void DoSecondary()
    {
        Debug.Log(weaponColor + " shoot secondary");
    }
}