using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject panelMenu;
    // Start is called before the first frame update
    void Start()
    {
        panelMenu.SetActive(false);
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
            Time.timeScale = 0;
        }
        else
        {
            panelMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    //TODO: Para cuando muera el personaje, llamara a este metodo
    public void usuarioMuere()
    {
        GameObject button = GameObject.Find("ButtonRestart"); //Busca el botton en la escena y lo pone true
        if (!panelMenu.activeInHierarchy)
        {
            panelMenu.SetActive(true);
            button.SetActive(true);
        }
        else
        {
            button.SetActive(true);
        }
        Time.timeScale = 0;
    }
}
