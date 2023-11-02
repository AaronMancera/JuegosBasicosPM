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

    //NOTE: Control de potenciadores con una clase de enumeracion
    PowerUpEnum powerUp;
    //NOTE: Control de potenciador de supergolpe
    //[SerializeField] private bool hasPowerUpStenght;
    private float powerupStrength;
    private GameObject indicadorPowerUp;
    //NOTE: Contro de potenciador de cohetes *ADICIONAL*
    //[SerializeField] private bool hasPowerUpRocket;
    [SerializeField]private GameObject rocketPrefab;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 5f;
        focalPoint = GameObject.Find("FocalPoint");
        //hasPowerUpStenght = false;
        powerupStrength = 50f;
        indicadorPowerUp = gameObject.transform.GetChild(0).gameObject;
        indicadorPowerUp.SetActive(false);
        powerUp = PowerUpEnum.Normal;
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
        //indicadorPowerUp.SetActive(hasPowerUpStenght);
        if(powerUp==PowerUpEnum.RocketLauncher && Input.GetButton("Fire1")==true)
        {
            //TODO: Instancia un prefab del cohete que tenga un script que simplemente destruya con lo que impacte
            dispararCohetes();
            
        }


    }
    private void dispararCohetes() 
    {
        Instantiate(rocketPrefab,transform.position,Quaternion.Euler(0,0,0));
        powerUp = PowerUpEnum.Normal;
        indicadorPowerUp.SetActive(false);
    }
    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("PowerUp") && powerUp == PowerUpEnum.Normal)
        {
            Debug.Log(other.gameObject.transform.GetChild(0).gameObject.name); /*Coge el nombre del icono del poder*/
            switch (other.gameObject.transform.GetChild(0).gameObject.name)
            {
                case string c when c.Contains("0"): /* Se declara una variable local que se va a utilizar para hacerle un contains en el interior*/
                    Debug.Log("Holaaa");
                    powerUp = PowerUpEnum.SuperStrength;
                    StartCoroutine(PowerUpCountdownRoutine());
                    break;
                case string c when c.Contains("1"):
                    powerUp = PowerUpEnum.RocketLauncher;
                    break;
                case string c when c.Contains("2"):
                    powerUp = PowerUpEnum.SuperSlam;
                    StartCoroutine(PowerUpCountdownRoutine());
                    break;
            }
            indicadorPowerUp.SetActive(true);
            Destroy(other.gameObject);
           
            
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
        //if (hasPowerUpStenght && collision.gameObject.CompareTag("Enemy"))
        //{
        //    Vector3 awayFromPlayer = collision.transform.position - transform.position;
        //    collision.rigidbody.AddForce(awayFromPlayer*powerupStrength,ForceMode.Impulse);
        //}
        if (powerUp==PowerUpEnum.SuperStrength && collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 awayFromPlayer = collision.transform.position - transform.position;
            collision.rigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
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
        powerUp = PowerUpEnum.Normal;
        indicadorPowerUp.SetActive(false);
    }
    #endregion
}
