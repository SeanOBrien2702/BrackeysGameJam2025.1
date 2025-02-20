using NUnit.Framework.Constraints;
using UnityEngine;

[CreateAssetMenu(fileName = "BirdMissileAbility", menuName = "Ability/BirdMissileAbility", order = 0)]
public class BirdMissileAbility : Ability
{
    [SerializeField] private float cooldown = 6;
    [SerializeField] private int numAttacks = 8;
    [SerializeField] private Vector2 screenSize = new(8.5f, 6.5f);
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float animationTime = 1f;

    private float lastUseTime = 0;

    public override bool ShouldUse()
    {
        return Time.time - lastUseTime > cooldown;
    }

    public override void Use(Boss caster)
    {
        lastUseTime = Time.time;

        caster.CanCast = false;
        caster.Animator.SetBool("DroppingMissiles", true);
        caster.Animator.SetTrigger("DroppingMissilesTrigger");

        var missileTime = animationTime / numAttacks;

        for (var x = 0; x < numAttacks; x++)
        {
            new GoodTimer(missileTime * x, () =>
            {
                Instantiate(missilePrefab, GetRandomPosition(), Quaternion.identity);
            });
        }

        new GoodTimer(animationTime, () =>
        {
            caster.Animator.SetBool("DroppingMissiles", false);
            caster.CanCast = true;
        });
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-screenSize.x, screenSize.x), Random.Range(-screenSize.y, screenSize.y), 0);
    }
}