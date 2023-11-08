using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLeftController : MonoBehaviour
{
    private float speed;
    private float lineaDestroyZ; //El lugar donde vamos a coger de referencia para desaparecer el objeto
    //private AudioSource cameraAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        lineaDestroyZ = -13; //cordenada Z de destrucción del objeto
        //cameraAudioSource = Camera.main.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!JumpPlayerController.gameOver)
        {
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime); //movimiento a del objeto hacia el jugador
            if (gameObject.transform.position.z <= lineaDestroyZ)
            {
                ObstController.puntuacion += 1;
                Debug.Log(ObstController.puntuacion);
                Destroy(gameObject);
            }
        }
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    speed = 10f;
        //    cameraAudioSource.pitch = 2;
        //}
        //else if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    speed = 5f;
        //    cameraAudioSource.pitch = 1;
        //}
    }
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
