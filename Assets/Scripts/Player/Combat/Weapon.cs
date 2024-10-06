using UnityEngine;

public enum WeaponType { Basic, Aimed }

public abstract class Weapon : MonoBehaviour
{
    public EntityColor weaponColor;
    public WeaponType weaponType;

    public virtual void DoPrimary()
    {
        Debug.Log(weaponColor + " primary");
    }
    public virtual void DoSecondary()
    {
        Debug.Log(weaponColor + " secondary");
    }
}