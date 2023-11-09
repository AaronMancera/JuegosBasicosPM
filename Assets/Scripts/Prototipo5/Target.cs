using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    private GameManager gameManager;
    [SerializeField] private int pointValue;
    [SerializeField] private ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        //targetRb.AddForce(Vector3.up * Random.Range(12, 16), ForceMode.Impulse);
        //targetRb.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10),Random.Range(-10, 10), ForceMode.Impulse);
        //transform.position = new Vector3(Random.Range(-4, 4), -6);
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// Devolvera una vector que actuara como fuerza de impuslo de manera aleatoria
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    /// <summary>
    /// Devolvera una float que actuara de fuerza de rotacion de manera aleatoria
    /// </summary>
    /// <returns></returns>
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    /// <summary>
    /// Devolvera un vector que actuara de posicion de instanciamiento aleatorio
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    /// <summary>
    /// Meotodo para que el ratón + click izquierdo interactue con el objeto, dando puntos al ser destruido
    /// </summary>
    //private void OnMouseDown()
    //{
    //    if (gameManager.isGameActive)
    //    {
    //        Destroy(gameObject);
    //        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    //        gameManager.UpdateScore(pointValue);
    //    }
    //}

    /// <summary>
    /// Metodo que se llamara desde el gameobject para destruir el objeto
    /// </summary>
    public void OnPlayerDestroyMe()
    {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.UpdateScore(pointValue);
    }

    /// <summary>
    /// El trigger de un plano inferior actuara con el gameobject y eliminara el objeto, restando una vida
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.RestarUnavida();
        }
    }



}
