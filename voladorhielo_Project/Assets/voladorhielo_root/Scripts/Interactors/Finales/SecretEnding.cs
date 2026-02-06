using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretEnding : MonoBehaviour
{
    public int sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && SecretItem.collected)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
