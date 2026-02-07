using UnityEngine;

public class FlyEnemyAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Transform player;
    [SerializeField] private float minDistance;

    [SerializeField] private Transform homePoint;

    private bool hasAlerted = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        float homeDistance = Vector2.Distance(transform.position, homePoint.position);
        if (playerDistance < minDistance && !hasAlerted)
        {
            animator.SetTrigger("Alert");
            hasAlerted = true;
        }
        animator.SetBool("Fly", homeDistance > 0.05f);
        if (homeDistance <= 0.05f)
        {
            hasAlerted = false;
        }
    }

    private void OnDestroy()
    {
        if (animator != null)
            animator.SetTrigger("Death");
    }
}
