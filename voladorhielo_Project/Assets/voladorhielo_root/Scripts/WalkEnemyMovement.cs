using UnityEngine;

public class WalkEnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] int startingPoint;
    [SerializeField] float speed;
    private bool isFacingRigth = true;

    int i;
    void Start()
    {
        transform.position = points[startingPoint].position;
    }
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
           
            i++;
            
            if (i == points.Length)
            {
                i = 0;
            }
            Flip();
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    void Flip ()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRigth = !isFacingRigth;
    }
}
