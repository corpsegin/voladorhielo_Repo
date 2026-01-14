using UnityEngine;

public class AudioManager : MonoBehaviour
{
   //declaracion singleton
    private static AudioManager instance; //definicion de la fortaleza de datos

    public static AudioManager Instance
    {



        get
        {
            if (instance == null) Debug.Log("No hay Game Manager");
            return instance;
        }
        //fin singleton

    }
    //TODAS LAS VARIABLES DE LA FORTALEZA DEBEN SER PUBLICAS
    public AudioSource musicSource;
    public AudioSource SFXSource;
    public AudioClip[] musicLibrary;
    public AudioClip[] sfxLibrary;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        SFXSource.PlayOneShot(sfxLibrary[sfxToPlay]);
    }


}
