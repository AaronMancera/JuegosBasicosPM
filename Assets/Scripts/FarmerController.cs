using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmerController : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    private float initialAttackSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject prefabBala;

    private float horizontalInput;
    private float verticalInput;
    private bool action1;

    //TODO: Cosas para el raton
    [SerializeField] private RectTransform canvas;
    [SerializeField] private RectTransform puntoDeMira;
    [SerializeField] private Vector3 mousePosition;
    

    // Start is called before the first frame update
    void Start()
    {
        initialAttackSpeed = attackSpeed;
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

        MirarRaton();
    }

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
    //TODO: Crear un metodo que rote al personaje a la posicion del raton
    private void MirarRaton()
    {

    }
    private void Shot()
    {
        attackSpeed-= Time.deltaTime;
        if (action1 && attackSpeed<=0)
        {
            Instantiate(prefabBala, gameObject.transform.position, Quaternion.identity);
            attackSpeed = initialAttackSpeed;
        }
    }
}
