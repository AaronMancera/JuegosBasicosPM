using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsScript : MonoBehaviour
{
    /// <summary>
    /// Atributo fundamental para determinar la vel de movimiento
    /// </summary>
    //NOTE: Script que debe estar dentro de todos los prefabs de animales que haya en el array del controller
    //private Vector3 lineaDespwan; //El lugar donde vamos a coger de referencia para desparecer el objeto
    private int speed;
    /// <summary>
    /// Clase GamePrototype2Controller que la neceitamos para poder hacer referencia a los metodos set para restar vida cuando pase la zona de juego
    /// </summary>
    //NOTE: Necesitamos el GamePrototype2Controller
    private GamePrototype2Controller gamePrototype2Controller;
    // Start is called before the first frame update
    void Start()
    {
        //lineaDespwan = new Vector3(0, 0, -17);
        speed = 5;
        //NOTE: Esto busca al empezar dentro de la escena un objeto que sea del tipo GamePrototype2Controller
        gamePrototype2Controller = FindAnyObjectByType<GamePrototype2Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //Desaparecer();

    }
    #region Metodo propio de los animales (en deshuso por el uso de las colisiones)

    //private void Desaparecer()
    //{
    //    if(gameObject.transform.position.z <= lineaDespwan.z)
    //    {
    //        Debug.Log("Llegue");
    //        Destroy(gameObject);
    //    }
    //}
    #endregion

    #region Colision
    //NOTE: Sustituye lo de eliminar la bala en todas las direcciones
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayZone"))
        {
            //NOTE: Hacemos el metodo que hay en el GamePrototype2Controller
            gamePrototype2Controller.restarVida(1);
            Destroy(gameObject);
        }
    }
    #endregion
}
