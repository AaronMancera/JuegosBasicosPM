using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACarController : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
