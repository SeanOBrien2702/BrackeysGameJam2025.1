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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == shooter) return;

        if(collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}