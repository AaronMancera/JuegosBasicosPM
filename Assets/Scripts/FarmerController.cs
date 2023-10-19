using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    private float initialAttackSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject prefabBala;

    [SerializeField] private float horizontalInput;
    [SerializeField] private bool action1;

    //[SerializeField] private float verticalInput;




    // Start is called before the first frame update
    void Start()
    {
        initialAttackSpeed = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        action1 = Input.GetButton("Fire1");
        MovimientoHorizontal();
        Shot();
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
            Vector3 tp = new Vector3(17 * rango, 1, -10);
            gameObject.transform.position = tp;
        }
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
