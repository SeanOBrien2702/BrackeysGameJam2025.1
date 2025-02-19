using UnityEngine;

[CreateAssetMenu(fileName = "BirdMissileAbility", menuName = "Ability/BirdMissileAbility", order = 0)]
public class BirdMissileAbility : Ability
{
    [SerializeField] private float cooldown = 6;
    [SerializeField] private float numAttacks = 8;
    [SerializeField] private float attackCooldown = 0.25f;
    [SerializeField] private Vector2 screenSize = new(8.5f, 6.5f);
    [SerializeField] private GameObject missilePrefab;

    int attacksUsed = 0;
    private float lastUseTime = 0;

    public override bool ShouldUse()
    {
        return Time.time - lastUseTime > cooldown;
    }

    public override void Use(Boss caster)
    {
        Debug.Log($"Casting ability bird missile" );

        lastUseTime = Time.time;
        attacksUsed = 0;

        caster.CanCast = false;       
        caster.Animator.SetBool("DroppingMissiles", true);
        caster.Animator.SetTrigger("DroppingMissilesTrigger");

        SpawnMissile(caster);
    }

    private void SpawnMissile(Boss caster)
    {
        if(attacksUsed <= numAttacks)
        {
            new GoodTimer(attackCooldown, () =>
            {
                Instantiate(missilePrefab, GetRandomPosition(), Quaternion.identity);
                attacksUsed++;
                SpawnMissile(caster);
            });
        }
        else
        {
            caster.CanCast = true;
            caster.Animator.SetBool("DroppingMissiles", false);
        }
            
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-screenSize.x, screenSize.x), Random.Range(-screenSize.y, screenSize.y), 0);
    }
}