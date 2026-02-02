using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpscript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            
           
        }
    }
}
