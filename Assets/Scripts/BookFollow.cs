using UnityEngine;

public class BookFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float followDistance = 0.1f; 

    void Update()
    {
        if (target == null) return;

        // Calculate the direction towards the target
        Vector3 targetPosition = target.position;
        float distance = Vector2.Distance(transform.position, targetPosition);

        
        if (distance > followDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition + offset, speed * Time.deltaTime);
        }
    }
}
