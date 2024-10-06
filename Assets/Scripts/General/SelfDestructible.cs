using UnityEngine;

public class SelfDestructible : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;

    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
