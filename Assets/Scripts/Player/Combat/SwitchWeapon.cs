using System;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    [NonSerialized] public int currentWeapon = 0;

    public void Initialize()
    {
        SelectWeapon(currentWeapon);

        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == 0 && weapon.TryGetComponent<Weapon>(out var weap0))
            {
                weap0.weaponColor = EntityColor.Red;
            }
            else if (i == 1 && weapon.TryGetComponent<Weapon>(out var weap1))
            {
                weap1.weaponColor = EntityColor.Blue;
            }
            else if (i == 2 && weapon.TryGetComponent<Weapon>(out var weap2))
            {
                weap2.weaponColor = EntityColor.Yellow;
            }

            i++;
        }
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