using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 cameraPos;
    [SerializeField] private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GetComponent<Camera>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + cameraPos;
    }
}
