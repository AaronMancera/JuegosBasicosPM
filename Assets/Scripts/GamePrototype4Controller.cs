using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePrototype4Controller : MonoBehaviour
{
    /// <summary>
    /// El controlador de rondas y del juego que usara los script adicionales para realizar ciertas acciones fundamentales para las rondas
    /// </summary>
    private SpawnEnemiesScript spawnEnemiesScript;
    [SerializeField] private int ronda;
    private GameObject [] listaDeEnemigos;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TMP_Text textoRonda;
    [SerializeField] private GameObject powerUpPrefab;


    private SpawnPowerUpScript spawnPowerUpScript;

    // Start is called before the first frame update
    void Start()
    {
        ronda = 0;
        spawnEnemiesScript = GetComponent<SpawnEnemiesScript>();
        spawnEnemiesScript.SetEnemyPrefab(enemyPrefab);
        spawnPowerUpScript = GetComponent<SpawnPowerUpScript>();
        spawnPowerUpScript.setPowerUpPrefab(powerUpPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        textoRonda.text = "" + ronda;
        listaDeEnemigos = GameObject.FindGameObjectsWithTag("Enemy");
        if(listaDeEnemigos.Length <= 0)
        {
            ronda += 1;
            spawnEnemiesScript.CrearEnemigo(ronda);
            spawnPowerUpScript.CrearPowerUp(ronda);
        }
    }
}
