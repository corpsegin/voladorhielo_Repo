using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] int loseHealth = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BadPick"))
            //acceso al singleton GameManager
            GameManager.Instance.PlayerHealth -= loseHealth;
        collision.gameObject.SetActive(false);
    }
}
