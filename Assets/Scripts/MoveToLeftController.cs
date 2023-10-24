using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLeftController : MonoBehaviour
{
    private float speed;
    private float lineaDestroyZ; //El lugar donde vamos a coger de referencia para desaparecer el objeto

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        lineaDestroyZ = -13;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (gameObject.transform.position.z <= lineaDestroyZ)
        {
            Destroy(gameObject);
        }
    }
}
