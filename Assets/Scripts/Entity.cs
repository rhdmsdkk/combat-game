using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public int health;

    public virtual void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
