using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePrototype2Controller : MonoBehaviour
{
    [SerializeField] MenuController menuController;
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
}
