using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;
    private float forwardInput;

    //NOTE: El gameObject vacio que contiene solo a la camara
    private GameObject focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 5f;
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        //rb.AddForce(Vector3.forward * speed * forwardInput);
        rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
}
