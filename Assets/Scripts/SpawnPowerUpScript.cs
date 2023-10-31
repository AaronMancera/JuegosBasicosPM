using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUpScript : MonoBehaviour
{
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
    public void setPowerUpPrefab(GameObject powerUpPrefab)
    {
        this.powerUpPrefab = powerUpPrefab;
    }
    private Vector3 GenerateSpawnPosition()
    {
        float PosX = Random.Range(-spawnRange, spawnRange);
        float PosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(PosX, 0, PosZ);
        return randomPos;
    }
    public void CrearPowerUp(int numEnemigos)
    {
        for (int i = 0; i < numEnemigos; i++)
        {
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), Quaternion.identity);
        }
    }
}
