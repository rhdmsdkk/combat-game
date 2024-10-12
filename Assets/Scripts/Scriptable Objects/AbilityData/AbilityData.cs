using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDataScriptableObject", menuName = "ScriptableObjects/AbilityDataScriptableObject")]
public class AbilityData : ScriptableObject
{
    [Header("General")]
    public int maxHealth;

    [Header("Explode")]
    public float explodeStaggerAmount;
    public int explodeDamage;
    public ParticleSystem explodeParticleSystem;

    [Header("Heal")]
    public int healAmount;
    public ParticleSystem healParticleSystem;
}