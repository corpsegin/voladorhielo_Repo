using UnityEngine;
using UnityEngine.UI;

public class ColdLogic : MonoBehaviour
{
    [SerializeField] ColdSystem coldSystem;
    [SerializeField] Image coldBarFill;

    void Update()
    {
        coldBarFill.fillAmount = coldSystem.currentCold / coldSystem.maxCold;
    }
}
