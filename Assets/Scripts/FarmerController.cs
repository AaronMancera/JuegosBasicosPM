using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class FarmerController : MonoBehaviour
{
    /// <summary>
    /// Movimiento
    /// </summary>
    [SerializeField] private float attackSpeed;
    private float initialAttackSpeed;
    [SerializeField] private float moveSpeed;

    private float horizontalInput;
    private float verticalInput;
    private bool action1;

    /// <summary>
    /// Movimiento del mouse y rotacion del jugador
    /// </summary>
    [SerializeField] private GameObject puntoDeMira;
    [SerializeField] private Camera mainCamera;
    //NOTE: Plane representa un plano infinito en el mundo
    Plane plane = new Plane(Vector3.up, -1); //se pone -1 para que no este debajo de nuestro plano en el juego

    /// <summary>
    /// Prefabs de proyectiles (comidas)
    /// </summary>
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private GameObject [] listaDeComidas;

    /// <summary>
    /// Vidas por colision entre jugador y animal
    /// </summary>
    //NOTE: Necesitamos el GamePrototype2Controller
    GamePrototype2Controller gamePrototype2Controller;


    // Start is called before the first frame update
    void Start()
    {
        initialAttackSpeed = attackSpeed;
        mainCamera = Camera.main;
        //NOTE: Esto busca al empezar dentro de la escena un objeto que sea del tipo GamePrototype2Controller
        gamePrototype2Controller = FindAnyObjectByType<GamePrototype2Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        action1 = Input.GetButton("Fire1");

        MovimientoHorizontal();
        Shot();
        MovimientoVertical();
        if (Time.timeScale == 1)
        {
            MirarRaton();
        }
    }

    #region Movimientos
    private void MovimientoHorizontal()
    {
        this.gameObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * horizontalInput);
        //NOTE: Tp en el lado contrario
        //if (gameObject.transform.position.x >= 18)
        //{
        //    gameObject.transform.position = new Vector3(-17, 0, -10);
        //}
        //else if (gameObject.transform.position.x <= -18)
        //{
        //    gameObject.transform.position = new Vector3(17, 0, -10);

        //}

        //NOTE: Este metodo lo que provoca que detecte cuando salga de los limites y que sepa a que lado tiene que tepear
        if (gameObject.transform.position.x >= 18 || gameObject.transform.position.x <= -18)
        {
            int rango = (int)-(gameObject.transform.position.x / 18);
            Vector3 tp = new Vector3(17 * rango, gameObject.transform.position.y, gameObject.transform.position.z);
            gameObject.transform.position = tp;
        }
    }
    private void MovimientoVertical()
    {
        this.gameObject.transform.Translate(-Vector3.back * moveSpeed * Time.deltaTime * verticalInput);

        //NOTE: Este metodo lo que provoca que detecte cuando salga de los limites y que sepa a que lado tiene que tepear
        if (gameObject.transform.position.z >= 7 || gameObject.transform.position.z <= -12.5)
        {
            if (gameObject.transform.position.z > 0)
            {
                Vector3 tp = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,  -12f);
                gameObject.transform.position = tp;

            }
            else
            {
                Vector3 tp = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 6f);
                gameObject.transform.position = tp;


            }
        }
    }
    #endregion Movimientos

    #region MovimientoRatonYDisparo
    //TODO: Crear un metodo que rote al personaje a la posicion del raton
    private void MirarRaton()
    {
        // Obtén la posición del mouse en la pantalla.
        Vector3 mousePositionScreen = Input.mousePosition;

        //// Err: El objeto "Punto de mira" no se encuadra bien con el plano de juego
        //// Convierte la posición del mouse de la pantalla al mundo.
        ////Vector3 mousePositionWorld = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, 10.0f));
        ////puntoDeMira.transform.position =new Vector3(mousePositionWorld.x,1,mousePositionWorld.z);

        //FIX: Hemos creado un plano infinito donde se va a more la bola mediante un ray desde la camara del mundo hasta un plano (el generado infinito) y calcule la distancia automaticamente
        float distance; //el valor da igual, hace lo mismo en 0 como en 50
        Ray ray = mainCamera.ScreenPointToRay(mousePositionScreen);
        if(plane.Raycast(ray, out distance))
        {
            //Coge el punto del plano donde esta el mouse
            mousePositionScreen = ray.GetPoint(distance);
        }
        //NOTE: Pone el punto de mira en dicha posicion
        puntoDeMira.transform.position=mousePositionScreen;
        //NOTE: Coge dentro del jugador el gameobject del cuerpo y lo rota
        transform.GetChild(transform.childCount - 1).gameObject.transform.LookAt(puntoDeMira.transform);

    }
    private void Shot()
    {
        attackSpeed-= Time.deltaTime;
        if (action1 && attackSpeed<=0)
        {
            //Instantiate(prefabBala, gameObject.transform.position, Quaternion.identity);
            // FIX: Esto provoca que aparezca con una leve leve leve orientacion hacia arriba o hacia abajo de manera esporadica
            GameObject bala = Instantiate(prefabBala, gameObject.transform.position, transform.GetChild(transform.childCount - 1).gameObject.transform.rotation);
            //Le asignamos una comida aleatoria
            GameObject comida = Instantiate(listaDeComidas[Random.Range(0, listaDeComidas.Length)], gameObject.transform.position, transform.GetChild(transform.childCount - 1).gameObject.transform.rotation);
            comida.transform.parent = bala.transform;
            attackSpeed = initialAttackSpeed;
        }
    }
    #endregion MovimientoRatonYDisparo

    #region Colisiones
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //NOTE: Hacemos el metodo que hay en el GamePrototype2Controller
            gamePrototype2Controller.restarVida(1);
            //NOTE: Choca con el jugador y le restamos una vida y desaparece el animal
            Destroy(collision.gameObject);
        }
    }
    #endregion Colisiones
}
