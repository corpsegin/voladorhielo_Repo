using UnityEngine;

public class FlyEnemyAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Transform player;
    [SerializeField] private float minDistance;

    private bool wasAlerted = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // ALERTA solo una vez
        if (distance < minDistance && !wasAlerted)
        {
            animator.SetTrigger("Alert");
            wasAlerted = true;
        }

        // VOLANDO solo mientras persigue
        animator.SetBool("isFlying", distance < minDistance);

        // Reset cuando jugador se va
        if (distance >= minDistance)
        {
            wasAlerted = false;
        }
    }

    private void OnDestroy()
    {
        if (animator != null)
            animator.SetTrigger("Die");
    }
}
