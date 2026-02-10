using UnityEngine;

public class CartaInteractor : MonoBehaviour
{
    public GameObject CartaPanel;
    [SerializeField] GameObject GameUI;
    public void ClosePanel()
    {
        Time.timeScale = 1;
        CartaPanel.SetActive(false);
        GameUI.SetActive(true);
    }
}
