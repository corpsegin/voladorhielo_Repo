using UnityEngine;

public class GameManager : MonoBehaviour
{
    //declaracion singleton
    private static GameManager instance; //definicion de la fortaleza de datos

    public static GameManager Instance
    {


        
     get
        {
            if (instance == null) Debug.Log("No hay Game Manager");
            return instance; 
        }
    //fin singleton
    
    }
    //TODAS LAS VARIABLES DE LA FORTALEZA DEBEN SER PUBLICAS
    public float PlayerHealth;
    public float maxHealth = 100;
    public int PlayerPoints;
    public int WinPoints;

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

    public void PointsUp(int gain) 
    {
        PlayerPoints += gain;
    }
}
