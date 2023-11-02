using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesScript : MonoBehaviour
{
    /// <summary>
    /// Controla la creacion de los enemigos durante el trascurso de la partida
    /// </summary>
    //private GameObject enemyPrefab;
    private float spawnRange = 9;

    private List<GameObject> enemiesPrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Se le pasa por parametro desde el controlador de juego el prefab para poder instanciarlo
    /// </summary>
    /// <param name="enemyPrefab"></param>
    //public void SetEnemyPrefab(GameObject enemyPrefab)
    //{
    //    this.enemyPrefab = enemyPrefab;
    //}

    /*
     *  ADICIONAL
     */

    /// <summary>
    /// Se le pasa por parametro la lista de enemigos desde el controlador de juego para poder instanciarlo
    /// </summary>
    /// <param name="enemiesPrefabs"></param>
    public void SetEnemiesPrefab(List<GameObject> enemiesPrefabs)
    {
        this.enemiesPrefabs = enemiesPrefabs;
    }
    /// <summary>
    /// Devuelve una posicion aleatoria para instanciar el objeto
    /// </summary>
    /// <returns></returns>
    private Vector3 GenerateSpawnPosition()
    {
        float PosX = Random.Range(-spawnRange, spawnRange);
        float PosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(PosX,0, PosZ);
        return randomPos;
    }
    /// <summary>
    /// Dependiendo del numero de enemigos, el bucle generara 1 o mas enemigos (determinado por las rondas)
    /// </summary>
    /// <param name="numEnemigos"></param>
    public void CrearEnemigo(int numEnemigos)
    {
        for (int i = 0; i < numEnemigos; i++)
        {
            //Instantiate(enemyPrefab, GenerateSpawnPosition(), Quaternion.identity);
            Instantiate(enemiesPrefabs[Random.Range(0,enemiesPrefabs.Count)], GenerateSpawnPosition(), Quaternion.identity);
        }
    }
}
