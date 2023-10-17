using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float velocidadGiro;

    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;

    private float aceleracion;
    private bool enMovimiento;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Direccion();
        Girar();

    }

    private void Direccion()
    {
        //this.gameObject.transform.position += new Vector3(0,0,Time.deltaTime*speed);

        this.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);


    }
    private void Girar()
    {
        
            this.gameObject.transform.Rotate(Vector3.up,velocidadGiro*horizontalInput*Time.deltaTime);

    }
}
