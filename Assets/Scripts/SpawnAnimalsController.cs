using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimalsController : MonoBehaviour
{
    [SerializeField] private GameObject [] animales;
    private Vector3 lineaSpwan; //El lugar donde vamos a coger de referencia para aparecer el objeto
    [SerializeField] private float segSpawnSpeed;
    private float startSegSpawnSpeed;


    // Start is called before the first frame update
    void Start()
    {
        lineaSpwan = new Vector3(0, 0, 25);
        startSegSpawnSpeed = segSpawnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        segSpawnSpeed -=Time.deltaTime;
        if (segSpawnSpeed <= 0f)
        {
            Quaternion angulo = new Quaternion(0, 90, 0,0); //Para que aparezcan dados la vuelta
            Instantiate(animales[Random.Range(0, animales.Length)], new Vector3(Random.Range(0, 17), 1, lineaSpwan.z), angulo);
            segSpawnSpeed = startSegSpawnSpeed;
        }
    }
}
