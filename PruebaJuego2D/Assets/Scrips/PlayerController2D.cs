using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Libreria para que funcione el NEw Input System

public class PlayerController2D : MonoBehaviour
{
    

    //Referencias generales
    [SerializeField] Rigidbody2D playerRb; //Ref al rigbody del player
    [SerializeField] PlayerInput playerInput; //REf al gestor del imput

    [Header("Movement Parameters")]
    private Vector2 moveInput;
    public float speed;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        //Autoreferenciar componentes: nombre de variable = GetComponent()
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        playerRb.velocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, 0);
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
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
       
    }

    #endregion


}
