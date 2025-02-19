using UnityEngine;

[CreateAssetMenu(fileName = "FlyUpAndSlamDownAbility", menuName = "Ability/FlyUpAndSlamDownAbility", order = 0)]
public class FlyUpAndSlamDownAbility : Ability
{

    [SerializeField] private float cooldown = 2;
    [SerializeField] private float squareRange = 10;
    [SerializeField] private Vector2 hitSize = new(4, 4);
    [SerializeField] private GameObject squareIndicatorPrefab;
    [SerializeField] private int damageDealt = 4;
    [SerializeField] private float timeInAir = 0.8f;
    [SerializeField] private float decscendAnimationTime = 1.6f;

    private float lastUseTime = 0;

    public override bool ShouldUse()
    {
        return Time.time - lastUseTime > cooldown;
    }

    public override void Use(Boss caster)
    {
        lastUseTime = Time.time;

        caster.Animator.SetBool("TakeOff", true);

        // Create a square sprite as a visual indicator of what will come
        var pos = new Vector2(Random.Range(-squareRange, squareRange), Random.Range(-squareRange, squareRange));

        var indicator = Instantiate(squareIndicatorPrefab, pos, Quaternion.identity);

        indicator.transform.localScale = hitSize;

        caster.SetCollisionEnabled(false);
        caster.SetCanCast(false);

        // Wait for the takeoff animation to take the boss off screen, then move to the new pos and play the animation
        // to come back down
        new GoodTimer(timeInAir, () =>
        {
            caster.transform.position = pos;

            caster.Animator.SetBool("TakeOff", false);
        });

        // 2 sec later (animation impact time), deal damage to the player if it's at that point, remove
        // the graphic, and play an impact particle effect
        new GoodTimer(timeInAir + decscendAnimationTime, () =>
        {
            Destroy(indicator);

            var hits = Physics2D.BoxCastAll(pos, new Vector2(hitSize.x, hitSize.y), 0, Vector2.up);

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject == caster.gameObject)
                {
                    continue;
                }
                if (hit.collider.TryGetComponent<PlayerController>(out var playerController))
                {
                    playerController.TakeDamage(damageDealt);
                }
            }

            caster.SetCollisionEnabled(true);
            caster.SetCanCast(true);
        });
    }

}