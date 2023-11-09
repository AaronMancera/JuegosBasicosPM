using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorteController : MonoBehaviour
{
    /*Adicional - Hacer clic y deslizar*/
    [SerializeField] LineRenderer trailPrefab = null;
    [SerializeField] private float limpiezaSpeed;
    private float distancia = 1;

    private LineRenderer traiActual;
    private List<Vector3> puntosDelTrail = new List<Vector3>();
    private void Start()
    {
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    DestroyTrailActual();
        //    CrearTrailActual();
        //    AddPunto();
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    AddPunto();
        //}
        //UpdatePuntosTrail();
        //LimpiarPuntosTrail();
    }

    /// <summary>
    /// Se llamara cuando queramos generar otra linea de corte
    /// </summary>
    public void DestroyTrailActual()
    {
        if (traiActual != null)
        {
            Destroy(traiActual.gameObject);
            traiActual = null;
            puntosDelTrail.Clear();
        }
    }
    /// <summary>
    /// Se llamara para crear una nueva linea
    /// </summary>
    public void CrearTrailActual()
    {

        traiActual = Instantiate(trailPrefab);
        traiActual.transform.SetParent(transform, true);
    }
    /// <summary>
    /// Se llamara para crear un nuevo punto para la linea
    /// </summary>
    public void AddPunto()
    {
        Vector3 mousePosition = Input.mousePosition;
        puntosDelTrail.Add(Camera.main.ViewportToWorldPoint(new Vector3(mousePosition.x / Screen.width, mousePosition.y / Screen.height, distancia)));
    }

    /// <summary>
    /// Se llamara para ir uniendo los puntos hasta llegar al final hasta que se no haya sufiencente spuntos o no exista el el rastro actual
    /// </summary>
    public void UpdatePuntosTrail()
    {
        if (traiActual != null && puntosDelTrail.Count > 1)
        {
            traiActual.positionCount = puntosDelTrail.Count;
            traiActual.SetPositions(puntosDelTrail.ToArray());
        }
        else
        {
            DestroyTrailActual();
        }
    }

    /// <summary>
    /// Se llamara para ir limpiando el reastro poco a poco
    /// </summary>
    public void LimpiarPuntosTrail()
    {
        float velLimpieza = limpiezaSpeed;
        //NOTE: Esto evita que podamos tener una linea demasiado larga, aumentado su velocidad de limpieza dependiendo de la longuitud de la linea
        if (puntosDelTrail.Count > 15 )
        {
            velLimpieza *= puntosDelTrail.Count/2;
        }
        else
        {
            velLimpieza = limpiezaSpeed;
        }
        float distanciaLimpieza = Time.deltaTime * velLimpieza;

        
        while (puntosDelTrail.Count > 1 && distanciaLimpieza > 0)
        {
            float distancia = (puntosDelTrail[1] - puntosDelTrail[0]).magnitude;
            if (distanciaLimpieza > distancia)
            {
                puntosDelTrail.RemoveAt(0);
            }
            else
            {
                puntosDelTrail[0] = Vector3.Lerp(puntosDelTrail[0], puntosDelTrail[1], distanciaLimpieza / distancia);
            }
            distanciaLimpieza -= distancia;
        }
    }

}
