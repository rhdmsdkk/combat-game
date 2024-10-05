using UnityEngine;
public class ExplodeWeapon : Weapon
{
    public override void DoPrimary()
    {
        Debug.Log(weaponColor + " explode primary");
    }
    public override void DoSecondary()
    {
        Debug.Log(weaponColor + " explode secondary");
    }
}