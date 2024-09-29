using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    void Start()
    {
        SelectWeapon(0);
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
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
