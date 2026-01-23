using UnityEngine;

public class FlyEnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] private Transform player;

    private bool isFacingRigth = true;

    [SerializeField] float knockbackForce = 5f;
    [SerializeField] float knockbackDuration = 0.3f;
    [SerializeField] private bool isKnockbacked;
    [SerializeField] private float knockbackTimer;

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            //Codigo de ataque
        }

        bool isPlayerRigth = transform.position.x < player.transform.position.x;
        Flip(isPlayerRigth);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //(collision.transform);
        }
    }

}
