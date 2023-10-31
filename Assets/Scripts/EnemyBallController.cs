using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallController : MonoBehaviour
{
    private float speed;
    private Rigidbody rb;
    private GameObject objetivo;
    private Vector3 direccionObjetivo;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        objetivo = GameObject.Find("Player");
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE: Soluciona un futuro error para cuando muera el player
        if (objetivo != null)
        {
            direccionObjetivo = objetivo.transform.position - transform.position;
            rb.AddForce(direccionObjetivo.normalized * speed);
        }
    }
    #region Trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayZone"))
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
