using UnityEngine;

public class objectInteractor : MonoBehaviour

{
    [SerializeField] GameObject Attack;
    [SerializeField] int TimesInteracted;
    [SerializeField] int neededTimesInteracted;
    [SerializeField] Animator ObjectAnimator;
    [SerializeField] bool open;
    

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
