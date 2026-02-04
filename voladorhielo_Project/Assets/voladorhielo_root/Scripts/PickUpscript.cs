using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpscript : MonoBehaviour
{
    public int PointSum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            AudioManager.Instance.PlaySFX(4);
            GameManager.Instance.PointsUp(PointSum);
            gameObject.SetActive(false);        }
    }
}
