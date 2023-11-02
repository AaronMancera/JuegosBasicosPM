using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    private GameObject[] enemiesList;
    // Start is called before the first frame update
    void Start()
    {
        enemiesList = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (objetivoMasCerca() != null)
        {
            transform.LookAt(objetivoMasCerca().transform);

            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }
        else { enemiesList = GameObject.FindGameObjectsWithTag("Enemy"); }
    }
    private GameObject objetivoMasCerca()
    {
        GameObject enemigoMasCerca = null;
        foreach (GameObject gO in enemiesList)
        {
            if (!enemigoMasCerca)
            {
                enemigoMasCerca = gO;
            }
            if (gO != null)
            {
                if (Vector3.Distance(transform.position, gO.transform.position) <= Vector3.Distance(transform.position, enemigoMasCerca.transform.position))
                {
                    enemigoMasCerca = gO;
                }
            }

        }
        return enemigoMasCerca;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 awayFromPlayer = collision.transform.position - transform.position;
            collision.rigidbody.AddForce(awayFromPlayer * 50, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
