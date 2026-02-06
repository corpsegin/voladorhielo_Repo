using UnityEngine;

public class HealthInteractor : MonoBehaviour
{
    [SerializeField] bool isPositive;
    [SerializeField] float quantity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           if (isPositive)
            {
                GameManager.Instance.PlayerHealth += quantity;
                AudioManager.Instance.PlaySFX(5);
            }
           else 
            {
                GameManager.Instance.PlayerHealth -= quantity;
                AudioManager.Instance.PlaySFX(2);
                gameObject.SetActive(false);
            }
        }
         
    }
}
