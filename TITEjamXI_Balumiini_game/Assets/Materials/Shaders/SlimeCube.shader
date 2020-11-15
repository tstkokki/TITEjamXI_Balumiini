Shader "Slime/SlimeCube"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (0.2, 0.2, 0.6, 1)
        _Intensity("Intensity", Color) = (1, 1, 1, 1)
        _Zoom("Zoom value", Float) = 1.0
        _TextureSpeed("Speed of voronoi", Float) = 1.0
        _Smoothness ("Smoothness", Range(0, 1)) = 0
        _Metallic ("Metalness", Range(0, 1)) = 0
        [HDR] _Emission ("Emission", color) = (0,0,0)

        _Amplitude ("Wave Size", Range(0,1)) = 0.4
        _Frequency ("Wave Freqency", Range(1, 8)) = 2
        _AnimationSpeed ("Animation Speed", Range(0,5)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert
		#pragma target 3.0

        #include "UnityCG.cginc"

        sampler2D _MainTex;

        fixed4 _Color;
        fixed4 _Intensity;

        half _Zoom;
        half _TextureSpeed;

        half _Smoothness;
        half _Metallic;
        half3 _Emission;

        float _Amplitude;
        float _Frequency;
        float _AnimationSpeed;

        struct Input 
        {
            float2 uv_MainTex;
        };

        float rand(float2 v)
        {
            return frac(sin(dot(v.xy, float2(20.123, 70.0132))) * 54321.12345123);
        }

        void vert(inout appdata_full data)
        {
            float4 modifiedPos = data.vertex;
            modifiedPos.y += sin(data.vertex.x * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;
            
            float3 posPlusTangent = data.vertex + data.tangent * 0.01;
            posPlusTangent.y += sin(posPlusTangent.x * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;

            float3 bitangent = cross(data.normal, data.tangent);
            float3 posPlusBitangent = data.vertex + bitangent * 0.01;
            posPlusBitangent.y += sin(posPlusBitangent.x * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;

            float3 modifiedTangent = posPlusTangent - modifiedPos;
            float3 modifiedBitangent = posPlusBitangent - modifiedPos;

            float3 modifiedNormal = cross(modifiedTangent, modifiedBitangent);
            data.normal = normalize(modifiedNormal);
            if(data.vertex.y > 0)
            {
                data.vertex = modifiedPos;
            }
            data.texcoord *= _Zoom;
        }


        void surf (Input i, inout SurfaceOutputStandard o) 
        {
            float4 col = float4 (0.0, 0.0, 0.0, 0.0);

            float2 iPos = floor(i.uv_MainTex);
            float2 fPos = frac(i.uv_MainTex);

            float minDist = 1.0;

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    float2 neighbor = float2((float)x, (float)y);

                    float2 p = rand(iPos + neighbor);

                    p = 0.5 + 0.5 * sin((_Time.y + 6.5435 * p)* _TextureSpeed);

                    float2 diff = neighbor + p - fPos;

                    float distance = length(diff);
                    minDist = min(minDist, distance);
                }
            }

            col += minDist;

            col *= _Color * _Intensity;

            o.Albedo = col.rgb;
            
            o.Metallic = sin((_Time.y + _Metallic) * 3.0)*0.4;
            o.Smoothness = _Smoothness;
            o.Emission = _Emission;
        }

        ENDCG
        
    }
}
