using UnityEngine;

public class FlyEnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] private Transform player;

    private bool isFacingRigth = true;

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            Attack();
        }

        bool isPlayerRigth = transform.position.x < player.transform.position.x;
        Flip(isPlayerRigth);
    }

    private void Attack()
    {
        Debug.Log("Atacar");
    }

    private void Flip (bool isPlayerRigth)
    {
        if((isFacingRigth && !isPlayerRigth) || (isPlayerRigth && !isFacingRigth))
        {
            isFacingRigth = !isFacingRigth;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
