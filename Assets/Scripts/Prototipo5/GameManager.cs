using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Audio;
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
    /*Adiconal - Volumen*/
    private float volumen;
    [SerializeField] private Slider volumenSlider;
    [SerializeField] private AudioMixer mixer;
    /*Adicional - Pausar Juego*/
    private bool juegoPausado;
    [SerializeField] private GameObject pauseScreen;
    /*Adicional - Estela que sigue donde donde pulser el jugador*/
    CorteController corteController;
    

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

        //NOTE:Le asignamos el metodo al slider
        volumenSlider.onValueChanged.AddListener(SetMusicLevel);
        volumenSlider.value = 0.5f;
        corteController=GetComponent<CorteController>();
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Puntuación: " + score;
        liveText.text = "Vidas: " + vidas;
        if(isGameActive && Input.GetKeyDown(KeyCode.Escape))
        {
            PausarJuego();
        }
        if (isGameActive && Input.GetMouseButton(0))
        {
            
            Raton();
        }

        //NOTE: El control del raton debe de hacerse aparte de si el juego esta activo ya que o sino genera una serie de bug a la hora de generar el corte
        if (Input.GetMouseButtonDown(0))
        {
            corteController.DestroyTrailActual();
            corteController.CrearTrailActual();
            corteController.AddPunto();
        }
        if (Input.GetMouseButton(0))
        {
            corteController.AddPunto();
        }
        corteController.UpdatePuntosTrail();
        corteController.LimpiarPuntosTrail();

    }
    /// <summary>
    /// Método públic donde se le pasara el score por parametro y lo actualizara
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(int score)
    {
        this.score += score;
    }

    /// <summary>
    /// Método que aplicara el fin de la partida
    /// </summary>
    //NOTE:Convertimos esta funcion en privada y ahora creamos una publica que va a ser restar una vida
    private void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }
    /// <summary>
    /// Método que restara un punto de vida y comprobara si es game over o no
    /// </summary>
    public void RestarUnavida()
    {
        vidas--;
        scoreText.text = "Vidas: " + vidas;
        if (vidas <= 0)
        {
            GameOver();
        }

    }
    /// <summary>
    /// Método que hara que se reinicie la escena
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// Método que empezara el juego con la dificultad que se le haya pasado por parametro
    /// </summary>
    /// <param name="difficulty"></param>
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
    /// <summary>
    /// El slider ira trayendo valores que iran modificando el volumen del mixer
    /// </summary>
    /// <param name="sliderValue"></param>
    private void SetMusicLevel(float sliderValue)
    {
        
        volumen = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("MusicVol", volumen);
    }
    public void PausarJuego()
    {
        if (!juegoPausado)
        {
            juegoPausado = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            juegoPausado = false;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }

    }
    /*Adicional - Permitir el arrastre y el pulsar a la vez*/
    private void Raton()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) )
        {
            Target target = hit.collider.gameObject.GetComponent<Target>();
            target.OnPlayerDestroyMe();
        }
    }

    /// <summary>
    /// Rutina que generara hasta que se acabe el juego isGameActive sea falso
    /// </summary>
    /// <returns></returns>
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



