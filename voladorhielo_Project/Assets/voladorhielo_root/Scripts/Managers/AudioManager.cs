using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //declaracion singleton

    public static AudioManager Instance;

    //TODAS LAS VARIABLES DE LA FORTALEZA DEBEN SER PUBLICAS
    [Header("Audio Source References")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip Arrays")]
    public AudioClip[] musicLibrary;
    public AudioClip[] sfxLibrary;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }

    public void PlayMusic(int musicToPlay)
    {
        musicSource.clip = musicLibrary[musicToPlay];
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    public void PlaySFX(int sfxToPlay)
    {
        Debug.Log("Sunea Sonid0");
        SFXSource.PlayOneShot(sfxLibrary[sfxToPlay]);
    }

    //CONTROLADOR DE VOLUMEN
    public Slider volumeSlider;

    public void ChangeVolumen()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

   private void Load()
   {
      volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
   }

    private void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
}
