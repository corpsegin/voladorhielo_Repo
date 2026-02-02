using UnityEngine;

public class objectInteractor : MonoBehaviour

{
    [SerializeField] GameObject Object;
    [SerializeField] GameObject Attack;
    [SerializeField] int TimesInteracted;
    [SerializeField] int neededTimesInteracted;

    private void OnTriggerEnter(Collider other)
    {
        if (Collision.gameObject.CompareTag("Attack"))
        {

        }

    }

    void Disappear()
    {
        if (TimesInteracted = neededTimesInteracted)
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
        
    }
}
