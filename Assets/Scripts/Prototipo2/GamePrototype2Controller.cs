using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePrototype2Controller : MonoBehaviour
{
    /// <summary>
    /// Clase MenuController que lo necesitamos para poder hacer referencia a las partes del menu
    /// </summary>
    [SerializeField] MenuController menuController;
    /// <summary>
    /// Atributos importantes para tener un control del juego sobre las vidas y las puntuaciones
    /// </summary>
    private int vida,score;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        vida = 3;
        score= 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        menuController.setScoreMenu(score);
        menuController.setVidaMenu(vida);
        if (vida<1)
        {
            menuController.usuarioMuere();
        }
    }
    #region Metodos de control del juego
    public void restarVida(int vida)
    {
        this.vida -= vida;
    }
    public void sumarScore(int score)
    {
        this.score += score;
    }
    public void restartTheGame()
    {
        //NOTE: Obtiene el nombre de la escena actual y la recarga
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion Metodos de control del juego
}
