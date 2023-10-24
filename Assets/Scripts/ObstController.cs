using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstController : MonoBehaviour
{
    [SerializeField] private List<GameObject> listObstaculosPrefabs;
    private float lineaSpwanZ; //El lugar donde vamos a coger de referencia para aparecer el objeto
    [SerializeField] private float segSpawnSpeed;
    private float startSegSpawnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startSegSpawnSpeed = segSpawnSpeed;
        lineaSpwanZ = 13;

    }

    // Update is called once per frame
    void Update()
    {
        segSpawnSpeed -= Time.deltaTime;
        if (segSpawnSpeed <= 0f)
        {
            GameObject gameObjectNuevo = listObstaculosPrefabs[Random.Range(0, listObstaculosPrefabs.Count)];
            //TODO: Ponerle el script de move left al objeto
            if (gameObjectNuevo.GetComponent<MoveToLeftController>() == null)
            {
                gameObjectNuevo.AddComponent<MoveToLeftController>();
            }
            gameObjectNuevo.tag = "Enemy";
            Quaternion angulo = new Quaternion(0, 90, 0, 0);
            Instantiate(gameObjectNuevo, new Vector3(1.5f, 0.27f, lineaSpwanZ), angulo);
            segSpawnSpeed = startSegSpawnSpeed;
        }

    }


}
