using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartCinematic : MonoBehaviour
{
    public VideoPlayer video;
    public Image panelNegro;
    public float duracionFade = 1f;

    void Start()
    {
        video.loopPointReached += FinVideo;
    }

    void FinVideo(VideoPlayer vp)
    {
        StartCoroutine(FadeYSalir());
    }

    IEnumerator FadeYSalir()
    {
        float tiempo = 0f;
        Color color = panelNegro.color;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            color.a = Mathf.Clamp01(tiempo / duracionFade);
            panelNegro.color = color;
            yield return null;
        }

        SceneManager.LoadScene(3);
    }
}
