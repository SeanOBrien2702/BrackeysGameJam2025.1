using UnityEngine;
using UnityEngine.iOS;

[CreateAssetMenu(fileName = "HitSquareAreaAbility", menuName = "Ability/HitSquareAreaAbility", order = 0)]
public class HitSquareAreaAbility : Ability
{

    [SerializeField] private float cooldown = 2;
    [SerializeField] private float squareRange = 10;
    [SerializeField] private float squareSize = 4;
    [SerializeField] private GameObject squareIndicatorPrefab;
    [SerializeField] private int damageDealt = 4;

    private float lastUseTime = 0;

    public override bool ShouldUse()
    {
        return Time.time - lastUseTime > cooldown;
    }

    public override void Use(GameObject caster)
    {
        lastUseTime = Time.time;

        // Create a square sprite as a visual indicator of what will come

        var pos = new Vector2(Random.Range(-squareRange, squareRange), Random.Range(-squareRange, squareRange));

        var indicator = Instantiate(squareIndicatorPrefab, pos, Quaternion.identity);

        // 1 sec later, deal damage to the player if it's at that point, remove
        // the graphic, and play an impact particle effect
        new GoodTimer(1, () =>
        {
            Destroy(indicator);

            var hits = Physics2D.BoxCastAll(pos, new Vector2(squareSize, squareSize), 0, Vector2.up);

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject == caster)
                {
                    continue;
                }
                if (hit.collider.TryGetComponent<PlayerController>(out var playerController))
                {
                    playerController.TakeDamage(damageDealt);
                }
            }
        });
    }

}