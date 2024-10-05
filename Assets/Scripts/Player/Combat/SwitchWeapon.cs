using System;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    [NonSerialized] public int currentWeapon = 0;

    void Start()
    {
        SelectWeapon(currentWeapon);
    }

    public void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex > 2)
        {
            return;
        }

        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == weaponIndex)
            {
                weapon.gameObject.SetActive(true);
                currentWeapon = weaponIndex;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
