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
               
            }
           else 
            {
                GameManager.Instance.PlayerHealth -= quantity;
                gameObject.SetActive(false);
            }
        }
         
    }
}
