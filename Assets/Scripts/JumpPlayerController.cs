using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigibody;
    private bool ground;
    [SerializeField] private float jumpForce;
     private bool actionJumpInput;
    void Start()
    {
        //NOTE:Recogemos el rigibody para poder hacer que salte
        rigibody = gameObject.GetComponent<Rigidbody>();
        ground = false;
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE: Por defecto en el project settings es en la tecla Space
        actionJumpInput = Input.GetButton("Jump");

        //NOTE: Siempre que salte y este ture el input de salto
        if (ground && actionJumpInput)
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

    //NOTE: Detectar de que colision esta saliendo
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ground = false;
        }
    }
    //NOTE: Detectar con que colision se encuentra chocando
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            ground = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Muerto");
            Time.timeScale = 0;
        }

    }
}
