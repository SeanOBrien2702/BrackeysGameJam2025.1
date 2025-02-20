using UnityEngine;

[CreateAssetMenu(fileName = "FeatherProjectileAbility", menuName = "Ability/FeatherProjectileAbility", order = 0)]
public class FeatherProjectileAbility : Ability
{
    [SerializeField] private float cooldown = 2;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int numProjectiles;
    [SerializeField] private int damage = 4;
    [SerializeField] private float angleSpread = 15f;
    [SerializeField] private float angleIncrement = 15f;
    [SerializeField] private float animationTime = 1.5f;

    protected float rotateAngle;
    private float lastUseTime = 0;

    public override bool ShouldUse()
    {
        return Time.time - lastUseTime > cooldown;
    }

    public override void Use(Boss caster)
    {
        lastUseTime = Time.time;

        caster.CanCast = false;
        caster.Animator.SetTrigger("Slash");

        new GoodTimer(animationTime, () =>
        {
            caster.CanCast = true;

            for (int i = 0; i < numProjectiles; i++)
            {
                var angleRad = rotateAngle * Mathf.Deg2Rad;

                Projectile projectile = Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);

                var target = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * 20f + caster.transform.position;

                projectile.Initialize(target, caster.gameObject.tag);
                rotateAngle += angleSpread;
            }
            rotateAngle += angleIncrement;
        });

    }
}