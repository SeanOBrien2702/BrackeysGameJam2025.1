using System;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public static event Action OnBossDefeated = delegate { };
    [SerializeField] HealthBarUI healthBar;
    [SerializeField] int startingHealth;
    int health;

    void Start()
    {
        health = startingHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        healthBar.UpdateHealth(health, startingHealth);
        if (health <= 0)
        {
            OnBossDefeated?.Invoke();
        }
    }
}