using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpellScript : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private bool isDrawing = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            StartCoroutine(DrawShape());
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
    }

    private IEnumerator DrawShape()
    {
        lineRenderer.positionCount = 1;
        int vertexCount = 1;
        lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.positionCount++;

        while (isDrawing)
        {
            lineRenderer.SetPosition(vertexCount, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            vertexCount++;
            lineRenderer.positionCount++;
            yield return new WaitForSeconds(.05f);
        }
    }
}
