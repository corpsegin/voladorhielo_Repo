using UnityEngine;

public class SecretItem : MonoBehaviour
{
    public static bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collected = true;

            Destroy(gameObject);
        }
    }
}
