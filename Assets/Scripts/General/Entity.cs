using UnityEngine;

public enum EntityColor { Red, Yellow, Blue, White }

public class Entity : MonoBehaviour
{
    public EntityColor entityColor;
    public int health;

    public virtual void ColorEntity(EntityColor color)
    {
        entityColor = color;
    }

    public virtual void TakeDamage(int dmg)
    {
        Debug.Log("ouch");

        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("died");

        Destroy(this);
    }
}
