using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [Header("Points Info")]
    [SerializeField]
    private GameObject pointPrefab;
    [SerializeField]
    private GameObject[] points;
    public int resolution;
    public float range;
    private float step;         

    [Header("Graph Info")]
    [SerializeField]
    private GraphType graphType = GraphType.X3;
    
    public enum GraphType
    {
        X3,
        SinX
    }


    
    private void InitPoints()
    {
        points = new GameObject[resolution];
        step = 2 * range / resolution;
        
        for (int i = 0; i < resolution; i++)
        {
            points[i] = Instantiate(pointPrefab, transform);
            points[i].transform.localScale = Vector3.one * step;
            Vector3 position = Vector3.zero;

            position.x = (i + 0.5f) * step - range;
            position.y = 0f;
            
            points[i].transform.localPosition = position;
        }
    }

    private void SetPoints()
    {
        switch (graphType)
        {
            case GraphType.X3:
                SetPointsX3();
                break;
            case GraphType.SinX:
                SetPointsSinX();
                break;
        }
    }

    private void SetPointsX3()
    {
        for (int i = 0; i < points.Length; i++) {
            GameObject point = points[i];
            Vector3 position = point.transform.localPosition;
            
            position.y = position.x * position.x * position.x;
            
            point.transform.localPosition = position;
        } 
    }

    private void SetPointsSinX()
    {
        for (int i = 0; i < points.Length; i++) {
            GameObject point = points[i];
            Vector3 position = point.transform.localPosition;

            position.y = Mathf.Sin((position.x + Time.time) * Mathf.PI);    // sin((x + 평행이동)pi)
            
            point.transform.localPosition = position;
        }  
    }
    private void Awake()
    {
        InitPoints();
    }

    private void Update()
    {
        SetPoints();
    }
}
