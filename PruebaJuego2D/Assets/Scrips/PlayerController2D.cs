using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Libreria para que funcione el NEw Input System

public class PlayerController2D : MonoBehaviour
{
    

    //Referencias generales
    [SerializeField] Rigidbody2D playerRb; //Ref al rigbody del player
    [SerializeField] PlayerInput playerInput; //REf al gestor del imput
    [SerializeField] Animator playerAnim; //Ref al animator para gestionar la transicion

    [Header("Movement Parameters")]
    private Vector2 moveInput;
    public float speed;
    [SerializeField] bool isFacingRight;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;
    //Variables para el GraundCheck
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        //Autoreferenciar componentes: nombre de variable = GetComponent()
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimations();
        GroundCheck();

        //Flip
        if (moveInput.x >0)
        {
            if (isFacingRight == false)
            {
                Flip();
            }
        }
        if (moveInput.x <0)
        {
            if (isFacingRight == true
                )
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        playerRb.velocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, 0);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight; //Nombre de bool = !nombre de bool (Cambio al estado contrario)
    }
    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    }

    void HandleAnimations()
    {
        //Conector de valores generales
        playerAnim.SetBool("IsJumping", !isGrounded);
        if (moveInput.x > 0 || moveInput.x < 0) playerAnim.SetBool("IsRunning", true);
        else playerAnim.SetBool("IsRunning", false);
        
    }

    #region Input Events

    //Para crear un evento
    //Se define PUBLIC sin tiop de dato (VOID) y con una referencia al input (Callback.Context)
    public void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
            
        }
       
    }

    #endregion


}
