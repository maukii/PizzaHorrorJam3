Shader "Custom/CameraSnapShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _GridSize("Grid Size", Float) = 16.0
        [Toggle]_UseFog("Use Fog", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalWS   : NORMAL;
                float2 uv         : TEXCOORD0;
                half   fogFactor  : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float _GridSize;
            float _UseFog;

            Varyings vert (Attributes v)
            {
                Varyings o;

                // Object -> World
                float4 worldPos = mul(GetObjectToWorldMatrix(), v.positionOS);

                // World -> View
                float3 viewPos = mul(UNITY_MATRIX_V, worldPos).xyz;

                // (full 3D quantize)
                // Snap to camera-space grid
                viewPos = floor(viewPos * _GridSize) / _GridSize;

                // (keeps depth continuous
                //float2 snappedXY = floor(viewPos.xy * _GridSize) / _GridSize;
                //viewPos.xy = snappedXY;

                // Back to world
                float4 snappedWorld = mul(UNITY_MATRIX_I_V, float4(viewPos, 1.0));

                // World -> Clip
                o.positionCS = mul(UNITY_MATRIX_VP, snappedWorld);
                o.normalWS = normalize(mul((float3x3)GetObjectToWorldMatrix(), v.normalOS));
                o.uv = v.uv;
                
                #if defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
                    o.fogFactor = ComputeFogFactor(o.positionCS.z);
                #else
                    o.fogFactor = 0;
                #endif

                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                if (_UseFog > 0.5)
                {
                    color.rgb = MixFog(color.rgb, i.fogFactor);
                }

                return color;
            }
            ENDHLSL
        }
    }
}