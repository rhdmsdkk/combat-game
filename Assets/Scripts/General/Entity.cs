using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EntityColor { Red, Yellow, Blue, White }

public class Entity : MonoBehaviour
{
    public EntityColor entityColor;
    public int health;

    public ColorData colorData;

    public virtual void ColorEntity(EntityColor color)
    {
        entityColor = color;
    }

    public virtual void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int hp)
    {
        health += hp;
    }

    protected virtual void Die()
    {
        Destroy(this);
    }

    public virtual void Stagger(float staggerAmount)
    {
        if (gameObject.TryGetComponent<Rigidbody>(out var rb))
        {
            Vector3 forceDirection = Random.insideUnitSphere * staggerAmount;
            forceDirection.y = staggerAmount * (3f / 2f);
            rb.AddForce(forceDirection, ForceMode.Impulse);
        }
    }
}
