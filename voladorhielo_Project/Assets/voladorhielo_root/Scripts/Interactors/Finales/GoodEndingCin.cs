using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;



public class GoodEndingCin : MonoBehaviour
{
    public VideoPlayer video;
    public Image fadeImage;
    public GameObject finalFrame;
    public GameObject fondo;

    public float flashSpeed = 4f;
    public float fadeOutSpeed = 0.8f;
    public float whiteHoldTime = 0.4f;

    void Start()
    {
        StartCoroutine(PlaySunCinematic());
        video.loopPointReached += OnVideoFinished;
    }

    IEnumerator PlaySunCinematic()
    {
        yield return StartCoroutine(Fade(0, 1, flashSpeed));
        yield return new WaitForSeconds(whiteHoldTime);
        fondo.SetActive(false);
        video.Play();
        StartCoroutine(Fade(1, 0, fadeOutSpeed));
        yield return null;
    }

    IEnumerator Fade(float from, float to, float speed)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * speed;

            Color c = fadeImage.color;
            c.a = Mathf.Lerp(from, to, t);
            fadeImage.color = c;

            yield return null;
        }
    }
    void OnVideoFinished(VideoPlayer vp)
    {
        finalFrame.SetActive(true);
        gameObject.SetActive(false);
    }

    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
