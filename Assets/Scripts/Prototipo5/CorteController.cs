using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorteController : MonoBehaviour
{
    /*Adicional - Hacer clic y deslizar*/
    public float width = 0.01f;
    public Color color = Color.red;
    private LineRenderer lr;
    private Vector3[] linePoints = new Vector3[2];

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (!lr) lr = gameObject.AddComponent<LineRenderer>();
        lr.material.color = color;
        lr.widthMultiplier = width;
        linePoints[0] = Vector3.zero;
        lr.positionCount = linePoints.Length;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (lr.enabled == false) lr.enabled = true;
            Camera c = Camera.main;
            Vector3 p = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, c.nearClipPlane));
            linePoints[1] = p;
            lr.SetPositions(linePoints);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
        }
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 30;
        GUILayout.Label("Screen: " + Input.mousePosition.ToString());
        GUILayout.Label("World: " + linePoints[1].ToString());
    }
}
