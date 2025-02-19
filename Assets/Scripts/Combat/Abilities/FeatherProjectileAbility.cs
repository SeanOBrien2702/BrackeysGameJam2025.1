using UnityEngine;

[CreateAssetMenu(fileName = "FeatherProjectileAbility", menuName = "Ability/FeatherProjectileAbility", order = 0)]
public class FeatherProjectileAbility : Ability
{
    [SerializeField] private float cooldown = 2;
    [SerializeField] private BossProjectile projectilePrefab;
    [SerializeField] private int numProjectiles;
    [SerializeField] private int damage = 4;
    [SerializeField] private float angleSpread = 15f;
    [SerializeField] private float angleIncrement = 15f;

    protected Vector3 projectileAngle = Vector3.zero;
    protected float rotateAngle;
    Quaternion quaternion;
    private float lastUseTime = 0;

    public override bool ShouldUse()
    {
        return Time.time - lastUseTime > cooldown;
    }

    public override void Use(Boss caster)
    {
        lastUseTime = Time.time;
        for (int i = 0; i < numProjectiles; i++)
        {
            projectileAngle.z = rotateAngle;
            quaternion.eulerAngles = projectileAngle;
            BossProjectile projectile = Instantiate(projectilePrefab, caster.transform.position, quaternion);
            projectile.Initialize(Vector2.zero, caster.gameObject.tag);
            rotateAngle += angleSpread;
        }
        rotateAngle += angleIncrement;
        caster.Animator.SetTrigger("Slash");       
    }
}