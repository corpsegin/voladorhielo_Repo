using System.Collections;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    [SerializeField]  GameObject Detectors;
    [SerializeField] float interval = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(cameras());
    }

    IEnumerator cameras()
    {
        while (true)
        { 
        Detectors.SetActive(!Detectors.activeSelf);
            yield return new WaitForSeconds(interval);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
