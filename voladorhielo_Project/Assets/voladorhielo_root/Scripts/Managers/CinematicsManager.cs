using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class IntroVideoSimple : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex = 1; // número de escena

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCamera = Camera.main;
        videoPlayer.aspectRatio = VideoAspectRatio.FitVertically;

        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void Update()
    {
        // Opcional: saltar video con cualquier tecla
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}

