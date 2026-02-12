using UnityEngine;
using System.Collections;

public class CartaCollider : MonoBehaviour
{
    public GameObject CartaPanel;
    public GameObject GameUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MostrarCarta());
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator MostrarCarta()
    {
        Time.timeScale = 0f;
        CartaPanel.SetActive(true);
        GameUI.SetActive(false);

        yield return new WaitForSecondsRealtime(3f);

        CartaPanel.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
    }
}
