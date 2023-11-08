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
    private bool soundDeathPlayed;


    //NOTE:*Ampliación* Extra jump
    [SerializeField] private int extraJump;
    [SerializeField] private float lineaDeDobleSalto;
    private int startExtraJump;
    //NOTE:*Ampliación* Animacion inicial
    private bool finAnimacionPrincipal;



    // Start is called before the first frame update
    void Start()
    {
        //Para que tenga la misma reaccion aqui y en mi casa
        Application.targetFrameRate = 60;
        //NOTE:Recogemos el rigibody para poder hacer que salte
        rigibody = gameObject.GetComponent<Rigidbody>();
        ground = false;
        anim = GetComponent<Animator>();
        gameOver = false;
        playerAudio = GetComponent<AudioSource>();
        startExtraJump=extraJump;
        lineaDeDobleSalto= transform.position.y + 0.5f;
        soundDeathPlayed = false;
        finAnimacionPrincipal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (finAnimacionPrincipal)
        {
            rigibody.constraints = RigidbodyConstraints.FreezePositionZ;
            //NOTE: Por defecto en el project settings es en la tecla Space
            //actionJumpInput = Input.GetButton("Jump");
            actionJumpInput = Input.GetKeyDown(KeyCode.Space);
            //NOTE: Game over
            isGameOver();

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
        else
        {
            AnimacionInicial();
        }
    }

    private void jump()
    {
        //NOTE: En rigibody se aplica una fuerza een una direcion (direccion * fuerza) y se aplica de manera secuencial
        //NOTE: Poner la jump force con 50 para saltar los obstaculos
        //rigibody.AddForce(transform.up * jumpForce);
        rigibody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
    /*Adicional*/
    private void exJump()
    {
        rigibody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        extraJump--;
        playerAudio.PlayOneShot(jumpSound, 1.0f);

    }

    private void isGameOver()
    {
        if (gameOver)
        {
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            if (explosionParticle.isPaused)
            {
                explosionParticle.Play();
            }
            if (polvoParticle.isPlaying)
            {
                polvoParticle.Stop();
            }
            if (!soundDeathPlayed)
            {
                playerAudio.PlayOneShot(crashSound, 1.0f);
                soundDeathPlayed = true;
            }

        }
    }
    /*Adicional*/
    private void AnimacionInicial()
    {

        rigibody.AddForce(transform.forward*2, ForceMode.Impulse);
    }
    public bool getFinAnimacionPrincipal()
    {
        return finAnimacionPrincipal;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            polvoParticle.Play();
            ground = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Muerto");
            //Time.timeScale = 0;
            gameOver = true;


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ZonaDeControl")
        {
            finAnimacionPrincipal = true;
        }
    }
    #endregion Colisiones
}
