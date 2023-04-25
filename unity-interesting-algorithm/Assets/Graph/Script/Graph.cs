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
        SinX,
        SumOfSin,
        MoveLikeWave,
        Ripple
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
        for (int i = 0; i < points.Length; i++) {
            GameObject point = points[i];
            Vector3 position = point.transform.localPosition;
            
            switch (graphType)
            {
                case GraphType.X3:
                    position.y = FunctionLibrary.X3(position.x);
                    break;
                case GraphType.SinX:
                    position.y = FunctionLibrary.SinX(position.x, Time.time);
                    break;
                case GraphType.SumOfSin:
                    position.y = FunctionLibrary.SumOfSin(position.x, Time.time);
                    break;
                case GraphType.MoveLikeWave:
                    position.y = FunctionLibrary.MoveLikeWave(position.x, Time.time);
                    break;
                case GraphType.Ripple:
                    position.y = FunctionLibrary.Ripple(position.x, Time.time);
                    break;
            }
            
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
