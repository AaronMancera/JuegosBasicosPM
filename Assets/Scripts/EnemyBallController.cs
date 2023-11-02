using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallController : MonoBehaviour
{
    /// <summary>
    /// Este controlador sirve para darle la capacidad al enemigo a ir tras el jugador
    /// </summary>
    private float speed;
    private Rigidbody rb;
    private GameObject objetivo;
    private Vector3 direccionObjetivo;
    //NOTE: Arregla este bool que la bola regrese de la caida
    private bool onGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objetivo = GameObject.Find("Player");
        if (gameObject.name.Contains("EnemyLvl1Ball"))
        {
            speed = 1f;
        }
        else
        {
            speed = 3f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (objetivo != null)
        {
            direccionObjetivo = objetivo.transform.position - transform.position;
        }
        else
        {
            //NOTE:Aqui si hubiese reaparicion de objetivo se reasignaria el objetivo, por ahora que simplemente vayan al centro del mapa y ya
            direccionObjetivo = new Vector3(0f, 0f, 0f) - transform.position;
        }
        ////NOTE: Soluciona un futuro error para cuando muera el player
        //if (onGround)
        //{


        //    rb.AddForce(direccionObjetivo.normalized * speed);

        //}
        


    }

    private void MoverseHAciaElObjetivo()
    {
        rb.AddForce(direccionObjetivo.normalized * speed);

    }
    #region Trigger

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayZone"))
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            MoverseHAciaElObjetivo();
        }
          
        //No funciona si lo hago directamente, no se por que 
        //onGround = other.CompareTag("Ground");

    }
    #endregion
    #region Colision

    #endregion
}
