using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public EntityColor weaponColor;

    public virtual void DoPrimary()
    {
        Debug.Log(weaponColor + " primary");
    }
    public virtual void DoSecondary()
    {
        Debug.Log(weaponColor + " secondary");
    }
}