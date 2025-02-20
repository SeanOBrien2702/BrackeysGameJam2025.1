using System;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private static readonly float DESTROY_DIST = 0.3f;

    [SerializeField] protected float speed;
    [SerializeField] protected float acceleration = 0;
    [SerializeField] int damage;
    Vector2 target;
    string shooter;

    private float currentSpeed;

    protected virtual void Update()
    {
        if (Vector2.Distance(target, transform.position) < DESTROY_DIST)
        {
            Destroy(gameObject);
            return;
        }

        currentSpeed += acceleration * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, currentSpeed * Time.deltaTime);
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        Vector3 targetVector = new(target.x, target.y, 0);
        Vector2 direction = (targetVector - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Initialize(Vector2 newTarget, string newShooter)
    {
        target = newTarget;
        shooter = newShooter;

        currentSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject collidedWith)
    {
        if (collidedWith.CompareTag(shooter)) return;
        if (collidedWith.GetComponent<Projectile>()) return;
        var parentDamageable = collidedWith.GetComponentInParent<IDamageable>();

        parentDamageable?.TakeDamage(damage);
        Destroy(gameObject);
    }
}