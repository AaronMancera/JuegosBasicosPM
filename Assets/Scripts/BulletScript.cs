using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed;
    private Vector3 lineaDespwan; //El lugar donde vamos a coger de referencia para desparecer el objeto

    // Start is called before the first frame update
    void Start()
    {
        speed = 20;
        lineaDespwan = new Vector3(0, 0, 27);

    }

    // Update is called once per frame
    void Update()
    {
        
        this.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Desaparecer();
    }

    private void Desaparecer()
    {
        if (gameObject.transform.position.z >= lineaDespwan.z)
        {
            Debug.Log("Adios Bala");
            Destroy(gameObject);
        }
    }


    //NOTE: Hay que poner el script en el mismo sitio donde se ubique el rigibody, sino no funciona
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
