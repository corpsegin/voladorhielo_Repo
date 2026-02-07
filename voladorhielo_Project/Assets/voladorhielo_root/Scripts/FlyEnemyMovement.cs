using UnityEngine;

public class FlyEnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] private Transform player;
    [SerializeField] private int health = 1;
    private Vector2 startPosition;
    private bool isFacingRigth = false;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
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
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
