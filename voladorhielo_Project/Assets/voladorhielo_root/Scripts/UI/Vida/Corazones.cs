using UnityEngine;
using UnityEngine.UI;

public class Corazones : MonoBehaviour
{
    [SerializeField] Image corazonesRotos;
    private float vidaMaxima;
    private PlayerController2D playerController;

    void Start()
    {
        playerController = GameManager.Instance.GetComponent<PlayerController2D>();
        vidaMaxima = GameManager.Instance.PlayerHealth;
    }

    void Update()
    {
        corazonesRotos.fillAmount = GameManager.Instance.PlayerHealth / vidaMaxima;
    }
    public void RecibirDaño(float cantidad = 1)
    {
        GameManager.Instance.PlayerHealth += cantidad;
    }

}
