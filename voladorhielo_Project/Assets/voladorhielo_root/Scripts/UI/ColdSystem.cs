using UnityEngine;
using UnityEngine.UI;

public class ColdSystem : MonoBehaviour
{
    [Header("Cold")]
    public Slider coldBar;
    public float maxCold = 100f;
    public float coldDrainSpeed = 5f;
    public float coldRecoverSpeed = 20f;

    [Header("Health")]
    public int hearts = 3;
    public float damageInterval = 1.5f;

    private float currentCold;
    private bool isNearCheckpoint = false;
    private float damageTimer;

    void Start()
    {
        currentCold = maxCold;
        coldBar.maxValue = maxCold;
        coldBar.value = currentCold;
    }

    void Update()
    {
        HandleCold();
        HandleDamage();
    }

    void HandleCold()
    {
        if (isNearCheckpoint)
        {
            // Recupera frío
            currentCold += coldRecoverSpeed * Time.deltaTime;
        }
        else
        {
            // Pierde frío
            currentCold -= coldDrainSpeed * Time.deltaTime;
        }

        currentCold = Mathf.Clamp(currentCold, 0, maxCold);
        coldBar.value = currentCold;
    }

    void HandleDamage()
    {
        if (currentCold <= 0 && hearts > 0)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                hearts--;
                damageTimer = 0;

                Debug.Log("Perdiste un corazón. Quedan: " + hearts);
            }
        }
        else
        {
            damageTimer = 0;
        }
    }

    // Detectar checkpoint
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            isNearCheckpoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            isNearCheckpoint = false;
        }
    }
}
