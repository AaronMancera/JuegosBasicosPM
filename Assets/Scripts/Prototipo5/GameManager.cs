using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1;
    private int score;
    public TMP_Text scoreText;
    public TMP_Text gameOverText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;

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
    }
    public void UpdateScore(int score)
    {
        this.score += score;
    }
    
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

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



