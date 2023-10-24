using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPictureController : MonoBehaviour
{
    [SerializeField] private int speed;
    private float posZDesplazarMaximo;
    private Vector3 puntoDeAparicion;
    // Start is called before the first frame update
    void Start()
    {
        posZDesplazarMaximo = -46.96f; //la coordenada Z a la que se va a desplazar como maximo a la izq
        puntoDeAparicion = new Vector3(-0.5f, 5, 32.87f); //el vector de aparicion
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE: Mueve a la izquiera el plano con la imagen
        gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        //NOTe: Cuando se pasa de la izquierda para repetirse, la movemos a la derecha del otro
        if (gameObject.transform.position.z <= posZDesplazarMaximo)
        {
            gameObject.transform.position = puntoDeAparicion;
        }
    }
}
