using System;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private static float DESTROY_DIST = 0.3f;

    [SerializeField] float speed;
    [SerializeField] int damage;
    Vector2 target;
    string shooter;

    void Update()
    {
        if (Vector2.Distance(target, transform.position) < DESTROY_DIST)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        RotateTowardsBoss();
    }

    private void RotateTowardsBoss()
    {
        Debug.Log("tets");
        Vector3 targetVector = new Vector3(target.x, target.y, 0);
        Vector2 direction = (targetVector - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Initialize(Vector2 newTarget, string newShooter)
    {
        target = newTarget;
        shooter = newShooter;
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

        var parentDamageable = collidedWith.GetComponentInParent<IDamageable>();

        parentDamageable?.TakeDamage(damage);
        Destroy(gameObject);
    }
}