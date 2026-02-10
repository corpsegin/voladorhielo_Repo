using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class healthSystemLab : MonoBehaviour

{
    [Header("Hearts")]
    public Image[] fullHearts;
    public int hitsToLoseHeart = 3; // cada cuántos golpes se pierde un corazón

    [Header("Death")]
    public Animator animator;
    public GameObject gameOverPanel;
    public PlayerController2D playerMovement;

    private int hearts;
    private int hitCounter = 0;
    private bool dead = false;

    void Start()
    {
        hearts = fullHearts.Length;
        gameOverPanel.SetActive(false);
    }

    // --- Golpe por enemigo ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dead) return;

        if (collision.collider.CompareTag("Enemy"))
        {
            RegisterHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead) return;

        if (collision.CompareTag("Enemy"))
        {
            RegisterHit();
       }

        
    }

    void RegisterHit()
    {
        hitCounter++;

        if (hitCounter >= hitsToLoseHeart)
        {
            LoseHeart();
            hitCounter = 0; 
        }
    }

    void LoseHeart()
    {
        hearts--;
        if (hearts >= 0)
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
   
}
