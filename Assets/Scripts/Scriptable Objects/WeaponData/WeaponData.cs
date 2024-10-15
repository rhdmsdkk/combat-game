using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataScriptableObject", menuName = "ScriptableObjects/WeaponDataScriptableObject")]
public class WeaponData : ScriptableObject
{
    [Header("Identity")]
    public string weaponName;
    public GameObject weapon;
}