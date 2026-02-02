using UnityEngine;

public class ColdSystem : MonoBehaviour
{
    public float maxCold = 100;
    public float currentCold;
    [SerializeField] private float coldDecrease = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCold = maxCold;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseCold();
    }

    void DecreaseCold()
    {
        currentCold -= coldDecrease * Time.deltaTime;
        currentCold = Mathf.Clamp(currentCold, 0, maxCold);

        if (currentCold <= 0)
        {
            //quita vida
        }
    }
}
