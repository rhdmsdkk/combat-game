using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataScriptableObject", menuName = "ScriptableObjects/PlayerDataScriptableObject")]
public class PlayerData : ScriptableObject
{
    [Header("General")]
    public int maxHealth;

    [Header("Abilities")]
    public GeneralAbilityData abilityData;
    public SingleAbilityData redAbility;
    public SingleAbilityData blueAbility;
    public SingleAbilityData yellowAbility;

    [Header("Weapons")]
    public WeaponData redWeapon;
    public WeaponData blueWeapon;
    public WeaponData yellowWeapon;

    [Header("Blank")]
    public SingleAbilityData blankAbility;
    public WeaponData blankWeapon;
}
