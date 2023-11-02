using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score;
    public TextMeshProUGUI scoreText;
    public TMP_Text gameOverText;
    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        gameOverText.gameObject.SetActive(false);

        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Puntuaci�n: " + score;
    }
    public void UpdateScore(int score)
    {
        this.score += score;
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
    }
    private IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
   
}



