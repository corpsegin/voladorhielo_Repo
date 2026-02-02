using UnityEngine;

public class objectInteractor : MonoBehaviour

{
    [SerializeField] GameObject Attack;
    [SerializeField] int TimesInteracted;
    [SerializeField] int neededTimesInteracted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            TimesInteracted++;
        }
        


        
    }

    void Disappear()
    {
        if (TimesInteracted >= neededTimesInteracted)
        {
            gameObject.SetActive(false);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Disappear();
    }
}
