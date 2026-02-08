using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ColdSystem2 : MonoBehaviour
{
    [Header("Cold")]
    public Image coldFillImage;
    public float coldDrainSpeed = 0.1f;
    public float coldRecoverSpeed = 0.3f;

    [Header("Hearts")]
    public Image[] fullHearts;
    public float damageInterval = 1.5f;

    [Header("Death")]
    public Animator animator;
    public GameObject gameOverPanel;
    public PlayerController2D playerMovement;

    float coldAmount = 1f;
    bool nearCheckpoint;
    float damageTimer;
    int hearts;
    bool dead = false;

    void Start()
    {
        hearts = fullHearts.Length;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (dead) return;

        HandleCold();
        HandleDamage();

        coldFillImage.fillAmount = coldAmount;
    }

    void HandleCold()
    {
        if (nearCheckpoint)
            coldAmount += coldRecoverSpeed * Time.deltaTime;
        else
            coldAmount -= coldDrainSpeed * Time.deltaTime;

        coldAmount = Mathf.Clamp01(coldAmount);
    }

    void HandleDamage()
    {
        if (coldAmount <= 0 && hearts > 0)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                LoseHeart();
                damageTimer = 0f;
            }
        }
        else
        {
            damageTimer = 0;
        }
    }

    void LoseHeart()
    {
        hearts--;

        fullHearts[hearts].enabled = false;

        if (hearts <= 0)
            Die();
    }

    void Die()
    {
        dead = true;
        playerMovement.enabled = false;
        animator.SetTrigger("Death");
        Invoke(nameof(ShowGameOver), 2f);
    }

    void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
            nearCheckpoint = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
            nearCheckpoint = false;
    }
}
