using Unity.VisualScripting;
using UnityEngine;

public class BossProjectile : Projectile
{
    [SerializeField] float projectLifetime = 1.5f;
    private void Start()
    {
        new GoodTimer(projectLifetime, () =>
        {
            Destroy(gameObject);
        });
    }

    protected override void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
