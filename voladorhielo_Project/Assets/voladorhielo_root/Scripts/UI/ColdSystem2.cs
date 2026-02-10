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
    float enemyDamageCooldown = 1f;
    float lastEnemyHitTime = -10f;


    float healTimer;
    public float healInterval = 10f;

    [Header("Enemy Cold Damage")]
    public float enemyColdDamagePercent = 0.25f;

    void Start()
    {
        Time.timeScale = 1;
        hearts = fullHearts.Length;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (dead) return;

        HandleCold();
        HandleDamage();
        HandleHealing();

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

    void HandleHealing()
    {
        if (nearCheckpoint && coldAmount >= 1f && hearts < fullHearts.Length)
        {
            healTimer += Time.deltaTime;

            if (healTimer >= healInterval)
            {
                GainHeart();
                healTimer = 0f;
            }
        }
        else
        {
            healTimer = 0;
        }
    }

    void GainHeart()
    {
        fullHearts[hearts].enabled = true;
        hearts++;
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
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("checkpoint"))
            nearCheckpoint = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("checkpoint"))
            nearCheckpoint = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyHit();
        }
    }

    public void EnemyHit()
    {
        if (Time.time - lastEnemyHitTime < enemyDamageCooldown) return;

        lastEnemyHitTime = Time.time;

        coldAmount -= enemyColdDamagePercent;
        coldAmount = Mathf.Clamp01(coldAmount);
    }
}

