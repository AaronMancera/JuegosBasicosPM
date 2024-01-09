using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstController : MonoBehaviour
{
    [SerializeField] private List<GameObject> listObstaculosPrefabs;
    //NOTE: El lugar donde vamos a coger de referencia para aparecer el objeto
    private float lineaSpwanZ;
    [SerializeField] private float segSpawnSpeed;
    private float startSegSpawnSpeed;
    //*Adicional*: Velocidad de movimiento
    private AudioSource cameraAudioSource;
    [SerializeField] private BackgroundPictureController backgroundPictureController1;
    [SerializeField] private BackgroundPictureController backgroundPictureController2;
    [SerializeField] private JumpPlayerController jumpPlayerController;
    [SerializeField] private GameObject nuevoObstaculo;
    [SerializeField] private MoveToLeftController moveToLeftController;

    //*Puntuacion*
    public static float puntuacion;
    // Start is called before the first frame update
    void Start()
    {
        startSegSpawnSpeed = segSpawnSpeed;
        lineaSpwanZ = 13;
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!JumpPlayerController.gameOver)
        {
            segSpawnSpeed -= Time.deltaTime;
            if (segSpawnSpeed <= 0f)
            {
                GameObject gameObjectNuevo = listObstaculosPrefabs[Random.Range(0, listObstaculosPrefabs.Count)];
                //NOTE: Esto es para meterle a los prefabs que queremos generar el script de que se muevan, por si no lo tienen
                if (gameObjectNuevo.GetComponent<MoveToLeftController>() == null)
                {
                    gameObjectNuevo.AddComponent<MoveToLeftController>();
                }
                //NOTE: Le asignamos el tag al gameobject nuevo
                gameObjectNuevo.tag = "Enemy";
                Quaternion angulo = Quaternion.Euler(0, -180, 0); //Esto es para girarlo y que este mirando para el jugador
                nuevoObstaculo = Instantiate(gameObjectNuevo, new Vector3(1.5f, 0.27f, lineaSpwanZ), angulo);
                segSpawnSpeed = startSegSpawnSpeed;
                moveToLeftController = nuevoObstaculo.GetComponent<MoveToLeftController>();
            }


            if (Input.GetKeyDown(KeyCode.LeftShift) && jumpPlayerController.getFinAnimacionPrincipal())
            {
                cameraAudioSource.pitch = 2;
                if (moveToLeftController != null)
                {
                    moveToLeftController.setSpeed(10f);
                }
                backgroundPictureController1.setSpeed(10f);
                backgroundPictureController2.setSpeed(10f);

            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                cameraAudioSource.pitch = 1;
                if (moveToLeftController != null)
                {
                    moveToLeftController.setSpeed(5f);
                }
                backgroundPictureController1.setSpeed(5f);
                backgroundPictureController2.setSpeed(5f);
            }
        }
        else
        {
            cameraAudioSource.pitch = 1;
            if (moveToLeftController != null)
            {
                moveToLeftController.setSpeed(5f);
            }
            backgroundPictureController1.setSpeed(5f);
            backgroundPictureController2.setSpeed(5f);
        }


    }


}
