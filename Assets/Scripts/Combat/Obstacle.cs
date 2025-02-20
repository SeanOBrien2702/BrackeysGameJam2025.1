using UnityEngine;

public class Obstacle : MonoBehaviour, IDamageable
{
    [SerializeField] int health;
    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0) Destroy(gameObject);
    }
}