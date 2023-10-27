using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed;
    //private Vector3 lineaDespwan; //El lugar donde vamos a coger de referencia para desparecer el objeto
    //NOTE: Necesitamos el GamePrototype2Controller
    GamePrototype2Controller gamePrototype2Controller;

    // Start is called before the first frame update
    void Start()
    {
        speed = 20;
        //lineaDespwan = new Vector3 (0, 0, 17);
        //NOTE: Esto busca al empezar dentro de la escena un objeto que sea del tipo GamePrototype2Controller
        gamePrototype2Controller = FindAnyObjectByType<GamePrototype2Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
        gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //FIX: Soluciona el ERR:Esto provoca que aparezca con una leve leve leve orientacion hacia arriba o hacia abajo de manera esporadica
        gameObject.transform.position = new Vector3(transform.position.x,1,transform.position.z);
        //Desaparecer();
    }

    //private void Desaparecer()
    //{
    //    if (gameObject.transform.position.z >= lineaDespwan.z)
    //    {
    //        Debug.Log("Adios Bala");
    //        Destroy(gameObject);
    //    }
    //}

    
    //NOTE: Hay que poner el script en el mismo sitio donde se ubique el rigibody, sino no funciona
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //NOTE: Hacemos el metodo que hay en el GamePrototype2Controller
            gamePrototype2Controller.sumarScore(50);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    //NOTE: Sustituye lo de eliminar la bala en todas las direcciones
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayZone"))
        {
            Destroy(gameObject);
        }
    }

}
