using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pausePanel;

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void QuitGame(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
