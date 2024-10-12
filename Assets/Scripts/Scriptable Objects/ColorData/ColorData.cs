using UnityEngine;

[CreateAssetMenu(fileName = "ColorDataScriptableObject", menuName = "ScriptableObjects/ColorDataScriptableObject")]
public class ColorData : ScriptableObject
{
    [Header("Materials")]
    public Material flashMaterial;
    public Material whiteMat;
    public Material redMat;
    public Material blueMat;
    public Material yellowMat;
}
