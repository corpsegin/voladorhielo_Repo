using UnityEngine;

public class WalkEnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] int startingPoint;
    [SerializeField] float speed;

    int i;
    void Start()
    {
        transform.position = points[startingPoint].position;
    }
    void Update()
    {
        PlatformMovement();
    }

    void PlatformMovement()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

    }
}
