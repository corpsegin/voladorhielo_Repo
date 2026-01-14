using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController2D : MonoBehaviour
{
    #region General Variables
    [Header("Movement & Jump Configuration")]
    [SerializeField] float speed = 8f;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck; //Posición del detector del suelo
    [SerializeField] float groundCheckRadius = 0.1f; //Radio del detector del suelo
    [SerializeField] LayerMask groundLayer; //Define la capa que puede tocar el detector del suelo

    [Header("Shoot Configuration")]
    [SerializeField] Transform shootPosition; //Ref al punto de disparo
    [SerializeField] GameObject projectile; //Ref al prefab del proyectil

    //Variables de referencia privadas
    Rigidbody2D playerRb; //Referencia al rigidbody del player
    Animator anim; //Referencia al controlador de animaciones del player
    PlayerInput input; //Referencia al cerebro de inputs del player
    Vector2 moveInput; //Referencia al valor pulsado de las teclas de movimiento
    bool canAttack; //Comprobador para determinar si se puede atacar

    [Header("Death configuration")]
    [SerializeField] Transform RespawnPoint;
    #endregion

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        canAttack = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Lógica de la detección del suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //Lógica de ejecución de animaciones
        //AnimationManagement();
        //Ejecución de la lógica del Flip
        if (moveInput.x > 0 && !isFacingRight) Flip();
        if (moveInput.x < 0 && isFacingRight) Flip();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Detectors"))
        {
            Respawn();
        }

        if (collision.gameObject.CompareTag("RespawnPoint"))
        {
            RespawnPoint = collision.transform;
        }
    }

    void Respawn()
    {
        playerRb.position = RespawnPoint.position;
    }
   

    void Movement()
    {
        playerRb.linearVelocity = new Vector2(moveInput.x * speed, playerRb.linearVelocity.y);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale; //Almacenamos el valor scale actual
        currentScale.x *= -1; //Cambiamos el valor de scale X al contrario actual
        transform.localScale = currentScale; //A la scale ACTUAL le pasamos la nueva modificada
        isFacingRight = !isFacingRight; //Cambiar el bool al valor contrario
    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        //AudioManager.Instance.PlaySFX(3);
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("Attacking");
        canAttack = false;
        float actualSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(0.8f);
        speed = actualSpeed;
        canAttack = true;
        yield return null;
    }

    void AnimationManagement()
    {
        //Gestión del cambio de animaciones: idle-jump-walk
        anim.SetBool("Jumping", !isGrounded);
        if (moveInput.x != 0) anim.SetBool("Running", true);
        else anim.SetBool("Running", false);
    }

    void ShootMagic()
    {
        //Llamar a un Instantiate de prefab de proyectil
        GameObject actualProjectile = Instantiate(projectile, shootPosition.position, Quaternion.identity);
        //Bullet bulletScript = actualProjectile.GetComponent<Bullet>();
       // bulletScript.isFacingRight = isFacingRight;
    }

    #region Input Methods
    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded) Jump();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && canAttack) StartCoroutine(Attack());
    }
    
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)ShootMagic();
    }

    #endregion
}
