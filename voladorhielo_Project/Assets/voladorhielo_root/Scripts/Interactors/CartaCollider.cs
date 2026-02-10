using UnityEngine;

public class CartaCollider : MonoBehaviour
{
    public GameObject CartaPanel;
    public GameObject GameUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0;
            CartaPanel.SetActive(true);
            GameUI.SetActive(false);
        }
    }
}
