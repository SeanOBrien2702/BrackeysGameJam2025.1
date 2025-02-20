using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public static event Action OnBossDefeated = delegate { };

    [NonSerialized] public bool CanCast = true;
    [NonSerialized] public bool IsValidTarget = true;

    [SerializeField] private HealthBarUI healthBar;
    [SerializeField] private int startingHealth;
    [SerializeField] private List<Ability> abilities = new();

    private int health;

    public Animator Animator
    {
        get;
        private set;
    }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    private void Start()
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
        if (!CanCast)
        {
            return;
        }

        foreach (var ability in abilities)
        {
            if (ability.ShouldUse())
            {
                Debug.Log($"Casting {ability}", ability);
                ability.Use(this);
            }
        }
    }
}