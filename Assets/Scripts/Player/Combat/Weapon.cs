using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public virtual void DoPrimary()
    {
        Debug.Log("primary");
    }
    public virtual void DoSecondary()
    {
        Debug.Log("secondary");
    }
}
