using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimalsController : MonoBehaviour
{
    [SerializeField] private GameObject[] animales;
    private Vector3 lineaSpwan1; //El lugar donde vamos a coger de referencia para aparecer el objeto vertical
    private Vector3 lineaSpwan2; //El lugar donde vamos a coger de referencia para aparecer el objeto horizontal

    [SerializeField] private float segSpawnSpeed;
    private float startSegSpawnSpeed;


    // Start is called before the first frame update
    void Start()
    {
        lineaSpwan1 = new Vector3(0, 0, 25);
        //desde 6.3z hasta el -10.36
        lineaSpwan2 = new Vector3(-20, 0, -25);
        startSegSpawnSpeed = segSpawnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        AparecerAnimal();
    }
    private void AparecerAnimal()
    {
        segSpawnSpeed -= Time.deltaTime;
        if (segSpawnSpeed <= 0f)
        {
            Quaternion angulo;
            int opcion = Random.Range(0, 2);
            switch (opcion)
            {
                case 0: //Caso Vertical
                    angulo = Quaternion.Euler(0, 180, 0); //Para que aparezcan dados la vuelta
                    Instantiate(animales[Random.Range(0, animales.Length)], new Vector3(Random.Range(-15, 16), 1, lineaSpwan1.z), angulo);
                    break;
                case 1: //Caso Horizontal
                    int posONeg = Random.Range(0, 2);
                    if (posONeg == 0)
                    {
                        angulo = Quaternion.Euler(0, 90, 0);
                        Instantiate(animales[Random.Range(0, animales.Length)], new Vector3(lineaSpwan2.x, 1, Random.Range(-10.36f, 6.3f)), angulo);
                    }
                    else
                    {
                        angulo = Quaternion.Euler(0, -90, 0);
                        Instantiate(animales[Random.Range(0, animales.Length)], new Vector3(lineaSpwan2.x * -1, 1, Random.Range(-10.36f, 6.3f)), angulo);
                    }
                    break;
            }
            segSpawnSpeed = startSegSpawnSpeed;


        }
    }

}
