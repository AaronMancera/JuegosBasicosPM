using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsScript : MonoBehaviour
{
    //NOTE: Script que debe estar dentro de todos los animales que haya en el array del controller
    private Vector3 lineaDespwan; //El lugar donde vamos a coger de referencia para desparecer el objeto
    private int speed;

    // Start is called before the first frame update
    void Start()
    {
        lineaDespwan = new Vector3(0, 0, -17);
        speed = 5;

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Desaparecer();

    }
    private void Desaparecer()
    {
        if(gameObject.transform.position.z <= lineaDespwan.z)
        {
            Debug.Log("Llegue");
            Destroy(gameObject);
        }
    }
}
