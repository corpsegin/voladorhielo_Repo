using UnityEngine;

public class MovingPlatform2D : MonoBehaviour
{
    [Header("Waypoints & Movement Configuration")]
    [SerializeField] Transform[] points; //Array de puntos hacia los que la plataforma se mueve
    [SerializeField] int startingPoint; //Número de punto desde el que empieza la plataforma
    [SerializeField] float speed; //Velocidad de movimiento de la plataforma
    bool canMove = true;
    bool isElevator;



    //Índice del Array de puntos que define el punto a "perseguir" de la plataforma
    int i; //i de índice

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Setear la posición inicial de la plataforma
        //La posición inicial es igual a startingPoint
        transform.position = points[startingPoint].position;

        isElevator = gameObject.name == "elevator";

        // Si es elevator, NO se mueve al empezar
        if (isElevator)
            canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            PlatformMovement();
    }

    void PlatformMovement()
    {
        //Calcular la distancia hacia el objetivo para que cuando sea muy corta cambie de objetivo
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++; //Sumar 1 al índice = cambiar al siguiente objetivo
            if (i == points.Length)
            {
                i = 0; //Resetear el índice cuando se llega al punto final
            }
        }
        //Mueve la plataforma desde su transform.position actual a...
        //... la posición del array que corresponda a lo que vale i en ese momento
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                collision.transform.SetParent(transform);
            }

            // Si es elevator, empieza a moverse
            if (isElevator)
            {
                canMove = true;
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);

            if (isElevator)
            {
                canMove = false;
            }
        }
    }

}
