using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private float abilityRadius = 5f;
    
    private Player player;

    private readonly List<Entity> entitiesRed = new();
    private readonly List<Entity> entitiesBlue = new();
    private readonly List<Entity> entitiesYellow = new();

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DetectEnemies();

            for (int i = 0; i < entitiesRed.Count; i++)
            {
                player.abilities[0]?.DoAbility(player, entitiesRed[i]);
            }

            for (int i = 0; i < entitiesBlue.Count; i++)
            {
                player.abilities[1]?.DoAbility(player,entitiesBlue[i]);
            }

            for (int i = 0; i < entitiesYellow.Count; i++)
            {
                player.abilities[2]?.DoAbility(player, entitiesYellow[i]);
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
        if (enemy.entityColor == EntityColor.White)
        {
            return;
        }

        if (enemy.entityColor == EntityColor.Red)
        {
            entitiesRed.Add(enemy);
        } 
        else if (enemy.entityColor == EntityColor.Blue)
        {
            entitiesBlue.Add(enemy);
        }
        else if (enemy.entityColor == EntityColor.Yellow)
        {
            entitiesYellow.Add(enemy);
        }
    }

    private void ResetConsumedColors()
    {
        entitiesRed.Clear();
        entitiesBlue.Clear();
        entitiesYellow.Clear();
    }
}
