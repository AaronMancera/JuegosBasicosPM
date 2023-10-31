using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    /// <summary>
    /// Lo que hace es rotar un empyobject que contiene la camara, provocando que de vueltas en circulos el game object 
    /// y la camara enfoque todo el rato el medio
    /// </summary>
    [SerializeField] private float rotationSpeed;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
