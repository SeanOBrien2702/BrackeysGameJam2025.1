using System.Collections;
using UnityEngine;

public class BirdMissile : MonoBehaviour
{
    [SerializeField] Transform missile;
    [SerializeField] float delay;
    [SerializeField] float movementDuration;
    [SerializeField] int damage;
    [SerializeField] private Vector2 hitSize = new(3, 3);
    [SerializeField] float fadeOutTime;

    void Start()
    {
        missile.gameObject.SetActive(false);
        new GoodTimer(delay, () =>
        {
            missile.gameObject.SetActive(true);
            StartCoroutine(MoveMissile());
        });
    }

    IEnumerator MoveMissile()
    {
        float time = 0;
        Vector3 startPos = missile.position;
        while (time < movementDuration)
        {
            time += Time.deltaTime;
            missile.position = Vector3.Lerp(startPos, transform.position, time / movementDuration);
            yield return null;
        }

        var hits = Physics2D.BoxCastAll(transform.position, new Vector2(hitSize.x, hitSize.y), 0, Vector2.up);
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<PlayerController>(out var playerController))
            {
                playerController.TakeDamage(damage);
            }
        }

        var sprites = GetComponentsInChildren<SpriteRenderer>();

        // Fade out sprites, then destroy
        float startTime = Time.time;
        while (Time.time - startTime < fadeOutTime) {
            float opacity = Mathf.Lerp(1, 0, (Time.time - startTime) / fadeOutTime);
            foreach (var sprite in sprites) {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, opacity);
            }
            yield return null;
        }

        Destroy(gameObject);
    }
}