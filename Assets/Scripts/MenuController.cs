using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject panelMenu,panelInGame;
    [SerializeField] private TMP_Text vidasMenu,vidasInGame,scoreMenu,scoreInGame;
    // Start is called before the first frame update
    void Start()
    {
        panelMenu.SetActive(false);
        panelInGame.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            usuarioPausaReanudaElJuego();
        }
    }
    private void usuarioPausaReanudaElJuego()
    {
        if (!panelMenu.activeInHierarchy)
        {
            panelMenu.SetActive(true);
            panelInGame.SetActive(false);

            Time.timeScale = 0;
        }
        else
        {
            panelMenu.SetActive(false);
            panelInGame.SetActive(true);
            Time.timeScale = 1;
        }
    }
    //TODO: Para cuando muera el personaje, llamara a este metodo
    public void usuarioMuere()
    {
        Debug.Log("Fin del juego");
        //GameObject button = GameObject.Find("ButtonRestart"); //Busca el botton en la escena y lo pone true
        GameObject button = panelMenu.transform.Find("ButtonRestart").gameObject; //Busca el botton en la escena y lo pone true

        if (!panelMenu.activeInHierarchy)
        {
            panelMenu.SetActive(true);
            panelInGame.SetActive(false);
            button.SetActive(true);
        }
        else
        {
            panelInGame.SetActive(false);
            button.SetActive(true);
        }
        Time.timeScale = 0;
    }
    //TODO: Para cuando dañen al personaje, llamara a este metodo
    public void setVidaMenu(int vida)
    {
        vidasMenu.text = ""+vida;
        vidasInGame.text = "" + vida;
    }
    //TODO: Para cuando sume un score el personaje, llamara a este metodo
    public void setScoreMenu(int score)
    {
        scoreMenu.text = "" + score;
        scoreInGame.text = "" + score;
    }
}
