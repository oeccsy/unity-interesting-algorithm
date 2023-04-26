Shader "Custom/PointSurface"
{
	Properties {
		_Smoothness ("Smoothness", Range(0,1)) = 0.5								// 아래에서 float _Smoothness를 Inspector에서 결정할 수 있도록 함
	}
	
    SubShader
    {
	    CGPROGRAM
		#pragma surface ConfigureSurface Standard fullforwardshadows				// ConfigureSurface : 셰이더를 구성하는 데 사용되는 방법, 이를 생성해야 함
		#pragma target 3.0															// 셰이더의 목표 수준과 품질에 대한 최소를 설정

        struct Input
        {
            float3 worldPos;														// ConfigureSurface에 input parameter로 전달할 Input 구조체
        };

        float _Smoothness;															// ConfigureSurface에서 surface의 Smoothness를 결정할 변수
        
        void ConfigureSurface (Input input, inout SurfaceOutputStandard surface)
	    {
        	surface.Albedo = saturate(input.worldPos * 0.5 + 0.5);					// -1~1 의 값이 0~1 의 값에 대응되도록 (*0.5 + 0.5)
			surface.Smoothness = _Smoothness;
		}
		ENDCG
    }
    FallBack "Diffuse"
}
