using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarrierFade : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeDuration = 1f;

    private bool alreadyTriggered = false;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !alreadyTriggered)
        {
            GetComponent<Collider2D>().enabled = false;
            alreadyTriggered = true;   
            StartCoroutine(FadeSequence());
        }
    }

    IEnumerator FadeSequence()
    {
        yield return StartCoroutine(Fade(0, 1));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0;
        Color c = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, time / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
    }
}
