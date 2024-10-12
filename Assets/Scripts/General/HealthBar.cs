using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private bool isPlayerHealthbar = false;
    [SerializeField] private Slider slider;

    private float maxHealth = 10f;

    private void Start()
    {
        if (isPlayerHealthbar)
        {
            maxHealth = FindAnyObjectByType<Player>().health;
        }
        else
        {
            maxHealth = GetComponentInParent<Transform>().GetComponentInParent<Entity>().health;
        }

        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}