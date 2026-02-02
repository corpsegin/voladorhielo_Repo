using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpscript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            AudioManager.Instance.PlaySFX(6);
            GameManager.Instance.PointsUp(1);
            gameObject.SetActive(false);    
        }
    }
}
