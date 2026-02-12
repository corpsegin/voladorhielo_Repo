using UnityEngine;

public class Seta : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    [SerializeField] Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.position = respawnPoint.position;
        }
    }
}
