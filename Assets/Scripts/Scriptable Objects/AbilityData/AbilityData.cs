using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDataScriptableObject", menuName = "ScriptableObjects/AbilityDataScriptableObject")]
public class AbilityData : ScriptableObject
{
    [Header("Explode")]
    public ParticleSystem explodeParticleSystem;

    [Header("Heal")]
    public ParticleSystem healParticleSystem;
}