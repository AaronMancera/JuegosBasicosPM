using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesController : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;
    private float spawnRange = 9;
    //TODO: Aplicar rondas
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab,GenerateSpawnPosition(),Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //NOTE: Metodo que devuelve una posicion aleatoria para la aparicion de un emeigo
    private Vector3 GenerateSpawnPosition()
    {
        float PosX = Random.Range(-spawnRange, spawnRange);
        float PosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(PosX,0, PosZ);
        return randomPos;
    }
}
