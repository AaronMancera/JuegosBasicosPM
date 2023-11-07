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
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem polvoParticle;
    //NOTE: Sonidos
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioSource playerAudio;

    //NOTE:*Ampliación*
    [SerializeField] private int extraJump;
    [SerializeField] private float lineaDeDobleSalto;
    private int startExtraJump;


    // Start is called before the first frame update
    void Start()
    {
        //NOTE:Recogemos el rigibody para poder hacer que salte
        rigibody = gameObject.GetComponent<Rigidbody>();
        ground = false;
        anim = GetComponent<Animator>();
        gameOver = false;
        playerAudio = GetComponent<AudioSource>();
        startExtraJump=extraJump;
        lineaDeDobleSalto= transform.position.y + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE: Por defecto en el project settings es en la tecla Space
        //actionJumpInput = Input.GetButton("Jump");
        actionJumpInput = Input.GetKeyDown(KeyCode.Space);

        //NOTE: Siempre que salte y este ture el input de salto
        if (ground && actionJumpInput && !gameOver)
        {
            jump();

        }
        //ERR: Hace los dos saltos a la vez por alguna extraña razon
        else if (!ground && actionJumpInput && !gameOver && extraJump > 0 && transform.position.y > lineaDeDobleSalto)
        {
            exJump();
        }
        ////NOTE: Siempre que salte y este ture el input de salto
        //if (ground && actionJumpInput)
        //{
        //    jump();

        //}
        if (extraJump <= 0 && ground)
        {
            extraJump = startExtraJump;

        }

    }

    private void jump()
    {
        //NOTE: En rigibody se aplica una fuerza een una direcion (direccion * fuerza) y se aplica de manera secuencial
        //NOTE: Poner la jump force con 50 para saltar los obstaculos
        //rigibody.AddForce(transform.up * jumpForce);
        rigibody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
    private void exJump()
    {
        rigibody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        extraJump--;
    }

    #region Colisiones
    //NOTE: Detectar de que colision esta saliendo
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ground = false;
            anim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            polvoParticle.Stop();
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
            explosionParticle.Play();
            polvoParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            polvoParticle.Play();
            ground = true;
        }
    }
    #endregion Colisiones
}
