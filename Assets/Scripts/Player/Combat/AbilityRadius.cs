using UnityEngine;

public class AbilityRadius : MonoBehaviour
{
    public float abilityRadius = 5f;
    public Player player;

    private int[] entityColors = new int[3]; // [red, blue, yellow]

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            entityColors[i] = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DetectEnemies();

            for (int i = 0; i < entityColors[0]; i++)
            {
                player.abilities[0]?.DoAbility();
            }

            for (int i = 0; i < entityColors[1]; i++)
            {
                player.abilities[1]?.DoAbility();
            }

            for (int i = 0; i < entityColors[2]; i++)
            {
                player.abilities[2]?.DoAbility();
            }

            ResetConsumedColors();
        }
    }

    private void DetectEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, abilityRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<Enemy>(out var enemy))
            {
                UpdateConsumedColors(enemy);

                enemy.ColorEntity(EntityColor.White);
            }
        }
    }

    private void UpdateConsumedColors(Enemy enemy)
    {
        if (enemy.entityColor == EntityColor.Red)
        {
            entityColors[0]++;
        } 
        else if (enemy.entityColor == EntityColor.Blue)
        {
            entityColors[1]++;
        }
        else if (enemy.entityColor == EntityColor.Yellow)
        {
            entityColors[2]++;
        }
    }

    private void ResetConsumedColors()
    {
        for (int i = 0; i < 3; i++)
        {
            entityColors[i] = 0;
        }
    }
}
