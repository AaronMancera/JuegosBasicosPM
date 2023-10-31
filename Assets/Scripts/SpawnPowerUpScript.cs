using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUpScript : MonoBehaviour
{
    /// <summary>
    /// Controla la creacion de powerup durante el trascurso de la partida
    /// </summary>
    private GameObject powerUpPrefab;
    private float spawnRange = 9;
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
    /// <param name="powerUpPrefab"></param>
    public void setPowerUpPrefab(GameObject powerUpPrefab)
    {
        this.powerUpPrefab = powerUpPrefab;
    }
    /// <summary>
    /// Devuelve una posicion aleatoria para instanciar el objeto
    /// </summary>
    /// <returns></returns>
    private Vector3 GenerateSpawnPosition()
    {
        float PosX = Random.Range(-spawnRange, spawnRange);
        float PosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(PosX, 0, PosZ);
        return randomPos;
    }
    /// <summary>
    /// Dependiendo del numero de potenciadores, el bucle generara 1 o mas powerUp (determinado por las rondas)
    /// </summary>
    /// <param name="numPowerUp"></param>
    public void CrearPowerUp(int numPowerUp)
    {
        for (int i = 0; i < numPowerUp; i++)
        {
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), Quaternion.identity);
        }
    }
}
