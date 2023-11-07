using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerController : MonoBehaviour
{
    private Rigidbody rigibody;
    private bool ground;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool actionJumpInput;
    public static bool gameOver;
    //NOTE: Animator controller que viene por defecto en el personaje
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //NOTE:Recogemos el rigibody para poder hacer que salte
        rigibody = gameObject.GetComponent<Rigidbody>();
        ground = false;
        anim = GetComponent<Animator>();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE: Por defecto en el project settings es en la tecla Space
        actionJumpInput = Input.GetButton("Jump");

        ////NOTE: Siempre que salte y este ture el input de salto
        //if (ground && actionJumpInput)
        //{
        //    jump();

        //}
    }
    private void FixedUpdate()
    {
        //NOTE: Siempre que salte y este ture el input de salto
        if (ground && actionJumpInput && !gameOver)
        {
            jump();

        }
    }
    private void jump()
    {
        //NOTE: En rigibody se aplica una fuerza een una direcion (direccion * fuerza) y se aplica de manera secuencial
        //NOTE: Poner la jump force con 50 para saltar los obstaculos
        rigibody.AddForce(transform.up * jumpForce);
    }

    #region Colisiones
    //NOTE: Detectar de que colision esta saliendo
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ground = false;
            anim.SetTrigger("Jump_trig");

        }
    }
    //NOTE: Detectar con que colision se encuentra chocando
    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Muerto");
            //Time.timeScale = 0;
            gameOver = true;
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            ground = true;
        }
    }
    #endregion Colisiones
}
