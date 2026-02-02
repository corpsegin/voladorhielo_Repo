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

    [Header("Crouch Configuration")]
    [SerializeField] Transform headCheck;
    [SerializeField] float headCheckRadius = 0.1f;
    [SerializeField] LayerMask detectorLayer;

    //Variables de referencia privadas
    Rigidbody2D playerRb; //Referencia al rigidbody del player
    BoxCollider2D playerCollider;
    Animator anim; //Referencia al controlador de animaciones del player
    PlayerInput input; //Referencia al cerebro de inputs del player
    Vector2 moveInput; //Referencia al valor pulsado de las teclas de movimiento
    Vector2 normalColliderSize;
    Vector2 normalColliderOffset;
    bool canAttack; //Comprobador para determinar si se puede atacar
    bool isCrouching;
    Dialogue currentDialogue;
    GameObject currentInteractable;


    [Header("Death configuration")]
    [SerializeField] Transform RespawnPoint;

    [Header("taken objects")]
    [SerializeField] int stairs = 0;
    [SerializeField] int key = 0;
    [SerializeField] int neededstairs = 0;
    [SerializeField] int neededkey = 0;

    #endregion

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        canAttack = true;
        playerCollider = GetComponent<BoxCollider2D>();
        normalColliderSize = playerCollider.size;
        normalColliderOffset = playerCollider.offset;
       

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
        AnimationManagement();
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
        if (collision.TryGetComponent(out Dialogue dialogue))
        {
            currentDialogue = dialogue;
        }

        if (collision.gameObject.CompareTag("Detectors"))
        {
            Respawn();
        }

        if (collision.gameObject.CompareTag("RespawnPoint"))
        {
            RespawnPoint = collision.transform;
        }

        if (collision.CompareTag("Key") || collision.CompareTag("Stair"))
        {
            currentInteractable = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Dialogue dialogue))
        {
            if (currentDialogue == dialogue)
                currentDialogue = null;
        }

        if (collision.gameObject == currentInteractable)
        {
            currentInteractable = null;
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
        AudioManager.Instance.PlaySFX(3);
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        canAttack = false;
        float actualSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(0.8f);
        speed = actualSpeed;
        canAttack = true;
        yield return null;
    }

    void Crouch()
    {
        isCrouching = true;

        playerCollider.size = new Vector2( normalColliderSize.x, normalColliderSize.y / 3f); 

        playerCollider.offset = new Vector2(
            normalColliderOffset.x, normalColliderOffset.y - normalColliderSize.y / 3f);
    }

    void StandUp()
    {
        playerCollider.size = normalColliderSize;
        playerCollider.offset = normalColliderOffset;
        isCrouching = false;
    }

    bool CanStandUp()
    {
        return !Physics2D.OverlapCircle(
            headCheck.position,
            headCheckRadius,
            detectorLayer);
    }

    void AnimationManagement()
    {
        //Gestión del cambio de animaciones: idle-jump-walk
        anim.SetBool("jump", !isGrounded);
        // if (moveInput.x != 0) anim.SetBool("walk", true);
        anim.SetBool("walk", moveInput.x != 0 && isGrounded);
        anim.SetBool("idle", moveInput.x == 0 && isGrounded);
        anim.SetBool("crouch", isCrouching);
        anim.SetBool("attack", !canAttack);
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
        if (context.performed) ShootMagic();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Crouch();
        }

        if (context.canceled && CanStandUp())
        {
            StandUp();
        }


        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && currentDialogue != null)
        {
            currentDialogue.Interact();
        }

        if (!context.performed || currentInteractable == null)
            return;

        if (currentInteractable.CompareTag("Key"))
        {
            key++;
        }
        else if (currentInteractable.CompareTag("Stair"))
        {
            stairs++;
        }

    }
    #endregion
}
