using Unity.VisualScripting;
using UnityEngine;

public class objectInteractor : MonoBehaviour

{
    [SerializeField] GameObject Attack;
    [SerializeField] int TimesInteracted;
    [SerializeField] int neededTimesInteracted;
    [SerializeField] Animator anim;
    [SerializeField] bool open;
    [SerializeField] Collider2D Collider;
    [SerializeField] GameObject Key;
   



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            TimesInteracted++;
        }
        
    }

    void Open()
    {
        if (!open && TimesInteracted >= neededTimesInteracted)
        {
            open = true;
            anim.SetTrigger("Open");
           Collider.GetComponent<Collider2D>().enabled = false;
        }

    }

    void death()
    {
        
    }


    void Start()
    {
        
    }

   
    // Update is called once per frame
    void Update()
    {
        Open();
    }
}
