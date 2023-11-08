using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    private float spawnRate = 1;
    private int score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gameOverText;
    public bool isGameActive;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject titleScreen;
    /*Adicional - Vidas*/
    private int vidas;
    [SerializeField] private TMP_Text liveText;

    // Start is called before the first frame update
    void Start()
    {
        //isGameActive = true;
        //gameOverText.gameObject.SetActive(false);
        //restartButton.gameObject.SetActive(false);

        //StartCoroutine(SpawnTarget());
        //score = 0;
        //UpdateScore(0);
        //titleScreen.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Puntuación: " + score;
        liveText.text = "Vidas: " + vidas;
    }
    public void UpdateScore(int score)
    {
        this.score += score;
    }

    //NOTE:Convertimos esta funcion en privada y ahora creamos una publica que va a ser restar una vida
    private void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }
    public void RestarUnavida()
    {
        vidas--;
        scoreText.text = "Vidas: " + vidas;
        if (vidas >= 0)
        {
            GameOver();
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        score = 0;
        vidas = 3;
        scoreText.text = "Vidas: " + vidas;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }
    private IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            //NOTE: Generacion de objetos
            Instantiate(targets[index]);

        }
    }
   
}



