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
    private GameObject[] listaDeEnemigos;
    //[SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TMP_Text textoRonda;
    [SerializeField] private GameObject powerUpPrefab;

    [SerializeField] private List<GameObject> enemiesPrefabs;
    [SerializeField] private GameObject bossEnemy;



    private SpawnPowerUpScript spawnPowerUpScript;

    // Start is called before the first frame update
    void Start()
    {
        //NOTE: Lo capeo a 60 fps para que vaya igual en todos los ordenadores (en mi casa todo iba mas rapido y la ia te seguia muy de cerca, ya funciona como en el ordenador de trabajo)
        Application.targetFrameRate = 60;
        ronda = 0;
        spawnEnemiesScript = GetComponent<SpawnEnemiesScript>();
        //spawnEnemiesScript.SetEnemyPrefab(enemyPrefab);
        spawnEnemiesScript.SetEnemiesPrefab(enemiesPrefabs);
        spawnPowerUpScript = GetComponent<SpawnPowerUpScript>();
        spawnPowerUpScript.setPowerUpPrefab(powerUpPrefab);
        spawnEnemiesScript.SetEnemyBoss(bossEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        textoRonda.text = "" + ronda;
        listaDeEnemigos = GameObject.FindGameObjectsWithTag("Enemy");
        if (listaDeEnemigos.Length <= 0)
        {
            ronda += 1;
            if (ronda % 5 == 0) /*Cada 5 rondas aparecera un boss*/
            {
                spawnEnemiesScript.CrearBoss(ronda);
                spawnPowerUpScript.CrearPowerUp(ronda);

            }
            else
            {
                spawnEnemiesScript.CrearEnemigo(ronda);
                spawnPowerUpScript.CrearPowerUp(ronda);
            }
        }
    }
}
