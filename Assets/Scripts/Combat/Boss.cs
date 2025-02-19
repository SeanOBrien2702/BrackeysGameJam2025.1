using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public static event Action OnBossDefeated = delegate { };
    [SerializeField] HealthBarUI healthBar;
    [SerializeField] int startingHealth;
    int health;

    [SerializeField] private List<Ability> abilities = new();

    public Animator Animator
    {
        get;
        private set;
    }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        for (var x = 0; x < abilities.Count; x++)
        {
            abilities[x] = Instantiate(abilities[x]);
        }

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

    public void SetCollisionEnabled(bool isEnabled)
    {
        var colliders = GetComponentsInChildren<Collider2D>();

        foreach (var collider in colliders)
        {
            collider.enabled = isEnabled;
        }
    }

    private void Update()
    {
        foreach (var ability in abilities)
        {
            if (ability.ShouldUse())
            {
                ability.Use(this);
            }
        }
    }
}