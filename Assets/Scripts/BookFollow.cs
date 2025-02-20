using UnityEngine;

public class BookFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private SpringConfig springConfig;

    private Spring springX;
    private Spring springY;

    private void Awake()
    {
        springX = new Spring(springConfig, 0, 0);
        springY = new Spring(springConfig, 0, 0);
    }

    void Update()
    {
        if (target == null) return;

        springX.To(target.position.x + offset.x);
        springY.To(target.position.y + offset.y);

        springX.Tick(Time.deltaTime);
        springY.Tick(Time.deltaTime);

        transform.position = new Vector2(springX.Get(), springY.Get());

        // Calculate the direction towards the target
        // Vector3 targetPosition = target.position;
        // float distance = Vector2.Distance(transform.position, targetPosition);


        // if (distance > followDistance)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, targetPosition + offset, speed * Time.deltaTime);
        // }
    }
}
