using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    //[SerializeField] int loseHealth = 10;
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //// {
    //Acceso al singleton GameManager
    //   GameManager.Instance.PlayerHealth -= loseHealth;
    // collision.gameObject.SetActive(false);
    //}
    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "enemy") 
        {
            VidaporEnesimaVez.health --;
            if (VidaporEnesimaVez.health > 0) 
            {
                StartCoroutine(GetHurt());
            }

        }

        if(collision.transform.tag == "checkpoint") 
        { 
            VidaporEnesimaVez.health ++;
        }
    }

    IEnumerator GetHurt() 
    {
        Physics2D.IgnoreLayerCollision(8,9);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

}
