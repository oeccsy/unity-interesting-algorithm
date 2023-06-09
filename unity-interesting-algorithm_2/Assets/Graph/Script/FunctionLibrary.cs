using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate float Function(float x, float z, float t);

    private static Function[] functions = {X3, SinX, SumOfSin, MoveLikeWave, Ripple};

    public static Function GetFunction (Graph.GraphType type)
    {
        return functions[(int)type];
    }
    
    
    public static float X3(float x, float z, float t)
    {
        return x * x * x;
    }
    public static float SinX(float x, float z, float t)
    { 
        return Sin((x + z + t) * PI);               // sin((x + z + 평행이동)pi)
    }
    
    public static float SumOfSin(float x, float z, float t)
    {
        float y = Sin((x + t) * PI);
        y += Sin(2f * (z + t) * PI) * 0.5f;     // 두 Sin 함수의 합
        y += Sin((x + z + 0.25f * t) * PI);
        return y * (1f / 2.5f);
    }
    
    public static float MoveLikeWave(float x, float z, float t)
    {
        float y = Sin((x + 0.5f * t) * PI);     // 한 Sin 함수의 시간이 다르게 반영됨
        y += Sin(2f * (x + t) * PI) * 0.5f;
        return y * (2f / 3f);
    }

    public static float Ripple(float x, float z, float t)
    {                                               // Ripple을 아래의 절차를 따라 구현한다.
        float d = Sqrt(x * x + z * z);              // 원점으로부터의 거리 d(=distance) 
        float y = Sin((4f * d - t) * PI);           // 주기가 빠른 Sin 함수
        return y / (1f + 10f * d);                  // 원점으로부터의 거리 d에 따라 진폭이 줄어든다.
    }
}
