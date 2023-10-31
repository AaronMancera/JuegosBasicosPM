using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesScript : MonoBehaviour
{
    private GameObject enemyPrefab;
    private float spawnRange = 9;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //NOTE: Contructor para pasarle por parametro el prefab
    public void SetEnemyPrefab(GameObject enemyPrefab)
    {
        this.enemyPrefab = enemyPrefab;
    }   
    
    //NOTE: Metodo que devuelve una posicion aleatoria para la aparicion de un emeigo
    private Vector3 GenerateSpawnPosition()
    {
        float PosX = Random.Range(-spawnRange, spawnRange);
        float PosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(PosX,0, PosZ);
        return randomPos;
    }
    public void CrearEnemigo(int numEnemigos)
    {
        for (int i = 0; i < numEnemigos; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), Quaternion.identity);
        }
    }
}
