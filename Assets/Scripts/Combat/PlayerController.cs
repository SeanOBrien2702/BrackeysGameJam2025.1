using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static event Action OnPlayerDefeated = delegate { };
    [SerializeField] HealthBarUI healthBar;
    [SerializeField] int startingHealth;
    [SerializeField] Transform bookTransform;
    private Animator bookAnimator;
    int health;

    [SerializeField] Boss boss;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float attackCooldown;
    float attackTimer = 0;

    void Start()
    {
        health = startingHealth;
        bookAnimator = bookTransform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log($"Boss {boss} valid {boss.IsValidTarget}");
        attackTimer += Time.deltaTime;
        if(attackTimer > attackCooldown && boss.IsValidTarget)
        {
            attackTimer = 0;
            Projectile projectile = Instantiate(projectilePrefab, bookTransform.position, transform.rotation);
            projectile.Initialize(boss.transform.position, gameObject.tag);
            bookAnimator.SetTrigger("Shoot");
        }
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        healthBar.UpdateHealth(health, startingHealth);
        if(health <= 0)
        {
            OnPlayerDefeated?.Invoke();
        }
    }
}