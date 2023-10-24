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

        if (ground && actionJumpInput)
        {
            jump();

        }
    }
    private void jump()
    {
        rigibody.AddForce(transform.up * jumpForce);

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("No salto");

            ground = false;
        }
    }
    void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Salto");

            ground = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Muerto");
            
        }

    }
}
