using UnityEngine;

public class Projectile : MonoBehaviour
{
    private static float DESTROY_DIST = 0.3f;

    [SerializeField] protected float speed;
    [SerializeField] int damage;
    Vector2 target;
    string shooter;

    protected virtual void Update()
    {
        if (Vector2.Distance(target, transform.position) < DESTROY_DIST)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
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
        if (collidedWith.GetComponent<Projectile>()) return;
        var parentDamageable = collidedWith.GetComponentInParent<IDamageable>();

        parentDamageable?.TakeDamage(damage);
        Destroy(gameObject);
    }
}