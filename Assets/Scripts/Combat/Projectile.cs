using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    Transform target;
    string shooter;

    public Transform Target { get => target; set => target = value; }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void Initialize(Transform newTarget, string newShooter)
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