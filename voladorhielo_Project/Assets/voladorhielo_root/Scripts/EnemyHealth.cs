
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject Attack;
    [SerializeField] int TimesInteracted;
    [SerializeField] int neededTimesInteracted;
    [SerializeField] Animator anim;
    [SerializeField] GameObject Key;
    [SerializeField] float wait = 3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            TimesInteracted++;
        }

    }

    void enemy()
    {
       

        if (gameObject.CompareTag("Enemy"))
        {

            if (TimesInteracted >= 1)
            {
                anim.SetTrigger("Attacked");

            }

            if (TimesInteracted >= neededTimesInteracted)
            {
                anim.SetTrigger("Defeat");
                anim.SetTrigger("getkey");
                StartCoroutine(Activate());
                
            }


        }


    }





    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy();
    }

    IEnumerator Activate()
    {

        yield return new WaitForSeconds(wait);

        if (Key != null)
        {
            Key.SetActive(true);

        }
    }

}
