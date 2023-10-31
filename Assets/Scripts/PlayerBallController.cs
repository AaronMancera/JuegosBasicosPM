using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;
    private float forwardInput;

    //NOTE: El gameObject vacio que contiene solo a la camara
    private GameObject focalPoint;

    //NOTE: Control de potenciador
    [SerializeField] private bool hasPowerUp;
    private float powerupStrength;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 5f;
        focalPoint = GameObject.Find("FocalPoint");
        hasPowerUp = false;
        powerupStrength = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        //rb.AddForce(Vector3.forward * speed * forwardInput);
        rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
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
    //NOTE: Esto funcionaara para crear un subproceso secundrario que deshabilitara el poder en 7 segundos
    private IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
    }
    #endregion
}
