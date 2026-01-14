using UnityEngine;
using UnityEngine.UI;

public class HealthLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Image healthUI;
    

    // Update is called once per frame
    void Update()
    {
        HealthUpdater();
    }

    void HealthUpdater()
    {
        healthUI.fillAmount = GameManager.Instance.PlayerHealth / GameManager.Instance.maxHealth;
    }
}
