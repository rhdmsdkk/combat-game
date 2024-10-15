using UnityEngine;

[CreateAssetMenu(fileName = "SingleAbilityDataScriptableObject", menuName = "ScriptableObjects/SingleAbilityDataScriptableObject")]
public class SingleAbilityData : ScriptableObject
{
    [Header("Identity")]
    public string abilityName;
    public GameObject ability;
}
