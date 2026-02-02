using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGame()
    {
        Debug.Log("Has cerrado el juego");
        Application.Quit();
    }

    public void StartCredits()
    {
        creditsPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
}
