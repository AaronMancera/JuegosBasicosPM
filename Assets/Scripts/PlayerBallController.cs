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

    //NOTE: Control de potenciador de supergolpe
    [SerializeField] private bool hasPowerUpStenght;
    private float powerupStrength;
    private GameObject indicadorPowerUp;
    //NOTE: Contro de potenciador de cohetes *ADICIONAL*
    [SerializeField] private bool hasPowerUpRocket;
    private GameObject rocketPrefab;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 5f;
        focalPoint = GameObject.Find("FocalPoint");
        hasPowerUpStenght = false;
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
        indicadorPowerUp.SetActive(hasPowerUpStenght);
        if(hasPowerUpRocket && Input.GetButton("Fire1")==true)
        {
            //TODO: Instancia un prefab del cohete que tenga un script que simplemente destruya con lo que impacte
        }


    }
    private void dispararCohetes() 
    {
        
    }
    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp") && !hasPowerUpStenght)
        {
            switch (other.gameObject.transform.GetChild(0).gameObject.name)
            {
                case "PowerIcon":
                    hasPowerUpStenght = true;
                    break;
                case "FireIcon":
                    hasPowerUpRocket = true; 
                    break;
            }
            Destroy(other.gameObject);
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
        if (hasPowerUpStenght && collision.gameObject.CompareTag("Enemy"))
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
        hasPowerUpStenght = false;
        hasPowerUpRocket = false;
    }
    #endregion
}
