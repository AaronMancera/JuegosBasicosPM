using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    [SerializeField] private int difficulty; /*Este parametro hay que ajustarlo en el inspector*/
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Metodo de seleccion de dificultad
    /// </summary>
    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }

}
