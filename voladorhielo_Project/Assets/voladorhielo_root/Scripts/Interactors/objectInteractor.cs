using UnityEngine;

public class objectInteractor : MonoBehaviour

{
    [SerializeField] GameObject Attack;
    [SerializeField] int TimesInteracted;
    [SerializeField] int neededTimesInteracted;
    [SerializeField] Animator ObjectAnimator;
    [SerializeField] bool open;
    [SerializeField] Collider2D DoorCollider;
    

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
            ObjectAnimator.SetTrigger("Open");
           DoorCollider.GetComponent<Collider2D>().enabled = false;
        }
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
