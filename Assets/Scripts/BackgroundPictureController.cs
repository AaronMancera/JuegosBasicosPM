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
        posZDesplazarMaximo = -46.96f;
        puntoDeAparicion = new Vector3(-0.5f, 5, 32.87f);
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE: Mueve a la izquiera el 
        gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        //NOTe: Cuando se pasa de la izquierda para repetirse pilla el mismo sitio que el anterios
        if (gameObject.transform.position.z <= posZDesplazarMaximo)
        {
            gameObject.transform.position = puntoDeAparicion;
        }
    }
}
