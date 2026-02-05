using UnityEngine;

public class FlyEnemyKnockback : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.3f;

    private Rigidbody2D rb;
    private FlyEnemyMovement flyEnemy; // referencia al script original
    private bool isKnockbacked = false;
    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flyEnemy = GetComponent<FlyEnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isKnockbacked)
        {
            Vector2 knockDirection = (transform.position - player.position).normalized;
            StartCoroutine(ApplyKnockback(knockDirection));
        }
    }

    private System.Collections.IEnumerator ApplyKnockback(Vector2 direction)
    {
        isKnockbacked = true;
        flyEnemy.enabled = false;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
        flyEnemy.enabled = true;
        isKnockbacked = false;
    }
}
