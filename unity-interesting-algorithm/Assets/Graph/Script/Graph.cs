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
        points = new GameObject[resolution * resolution];           // 움직이는 평면을 위해 resolution * resolution의 Grid 생성
        step = 2 * range / resolution;
        
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)  // i : index, x : x축 index, z : z축 index
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;                                             // z축 index 증가
            }
            
            points[i] = Instantiate(pointPrefab, transform);
            points[i].transform.localScale = Vector3.one * step;
            Vector3 position = Vector3.zero;

            position.x = (x + 0.5f) * step - range;                 // x값이 (-range,range)에 대응하도록 함
            position.y = 0f;
            position.z = (z + 0.5f) * step - range;                 // z값이 (-range,range)에 대응하도록 함
            
            points[i].transform.localPosition = position;
        }
    }

    private void SetPoints(FunctionLibrary.Function f)
    {
        for (int i = 0; i < points.Length; i++) {
            GameObject point = points[i];
            Vector3 position = point.transform.localPosition;
            
            position.y = f(position.x, position.z, Time.time);
            
            point.transform.localPosition = position;
        }
    }

    private void Awake()
    {
        InitPoints();
    }

    private void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(graphType);
        SetPoints(f);
    }
}
