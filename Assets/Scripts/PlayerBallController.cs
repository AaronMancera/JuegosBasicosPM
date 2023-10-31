using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    /// <summary>
    /// Este controlador sirve para mover al jugador hacia delante y hacia atras en el sentido recto de la camara y además tiene control de los efectos que alteran al
    /// jugador
    /// </summary>
    private Rigidbody rb;
    private float speed;
    private float forwardInput;

    //NOTE: El gameObject vacio que contiene solo a la camara
    private GameObject focalPoint;

    //NOTE: Control de potenciador
    [SerializeField] private bool hasPowerUp;
    private float powerupStrength;
    private GameObject indicadorPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 5f;
        focalPoint = GameObject.Find("FocalPoint");
        hasPowerUp = false;
        powerupStrength = 50f;
        indicadorPowerUp = gameObject.transform.GetChild(0).gameObject;
        indicadorPowerUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        //rb.AddForce(Vector3.forward * speed * forwardInput);
        rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        //NOTE: Para el el powerUp este bien ubicado y tenga rotacion propia, y no las misma que el jugador
        indicadorPowerUp.transform.position=new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
        indicadorPowerUp.transform.rotation = Quaternion.Euler(0, 0, 0);
        //NOTE: simplemente con tener el boolean, este lo activara o lo desactivara
        indicadorPowerUp.SetActive(hasPowerUp);


    }
    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            //NOTE: Inicia el subproceso
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PlayZone"))
        {
            Destroy(gameObject);
        }   
    }
    #endregion
    #region Colisiones
    private void OnCollisionEnter(Collision collision)
    {
        if (hasPowerUp && collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 awayFromPlayer = collision.transform.position - transform.position;
            collision.rigidbody.AddForce(awayFromPlayer*powerupStrength,ForceMode.Impulse);
        }
    }
    #endregion
    #region Rutinas
    /// <summary>
    /// Esto funcionara para crear un subproceso secundrario, en este caso esperara 7 segundos y cuando acabe el contador, le quitara el poder
    /// </summary>
    /// <returns></returns>
    private IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
    }
    #endregion
}
