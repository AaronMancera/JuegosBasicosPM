using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsScript : MonoBehaviour
{
    //NOTE: Script que debe estar dentro de todos los animales que haya en el array del controller
    //private Vector3 lineaDespwan; //El lugar donde vamos a coger de referencia para desparecer el objeto
    private int speed;
    //NOTE: Necesitamos el GamePrototype2Controller
    GamePrototype2Controller gamePrototype2Controller;
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
    //private void Desaparecer()
    //{
    //    if(gameObject.transform.position.z <= lineaDespwan.z)
    //    {
    //        Debug.Log("Llegue");
    //        Destroy(gameObject);
    //    }
    //}
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
}
