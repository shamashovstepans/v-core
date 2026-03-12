Shader "Art/ToonMatcap"
{
    Properties
    {
        _MatcapTex ("Matcap Texture", 2D) = "white" {}
        _TintColor ("Tint Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "Queue" = "Geometry" }
        LOD 100

        Pass
        {
            Name "Matcap"
            Tags { "LightMode" = "UniversalForward" }
            Blend Off
            ColorMask RGB
            ZWrite On
            ZTest LEqual
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"
            TEXTURE2D(_MatcapTex);
            SAMPLER(sampler_MatcapTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MatcapTex_ST;
                float4 _TintColor;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalVS : TEXCOORD0;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.normalVS = mul((float3x3)UNITY_MATRIX_V, normalWS);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float3 n = normalize(input.normalVS);
                float2 matcapUV = n.xy * 0.5 + 0.5;
                half4 matcapSample = SAMPLE_TEXTURE2D(_MatcapTex, sampler_MatcapTex, matcapUV);
                half3 base = matcapSample.rgb * _TintColor.rgb;
                // Ensure minimum visibility (handles failed texture load or dark matcap)
                half3 minTint = _TintColor.rgb * 0.65;
                return half4(max(base, minTint), 1);
            }
            ENDHLSL
        }

        Pass
        {
            Name "Matcap2D"
            Tags { "LightMode" = "Universal2D" }
            Blend Off
            ColorMask RGB
            ZWrite On
            ZTest LEqual
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"
            TEXTURE2D(_MatcapTex);
            SAMPLER(sampler_MatcapTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MatcapTex_ST;
                float4 _TintColor;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalVS : TEXCOORD0;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.normalVS = mul((float3x3)UNITY_MATRIX_V, normalWS);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float3 n = normalize(input.normalVS);
                float2 matcapUV = n.xy * 0.5 + 0.5;
                half4 matcapSample = SAMPLE_TEXTURE2D(_MatcapTex, sampler_MatcapTex, matcapUV);
                half3 base = matcapSample.rgb * _TintColor.rgb;
                half3 minTint = _TintColor.rgb * 0.65;
                return half4(max(base, minTint), 1);
            }
            ENDHLSL
        }

        Pass
        {
            Name "MatcapUnlit"
            Tags { "LightMode" = "SRPDefaultUnlit" }
            Blend Off
            ColorMask RGB
            ZWrite On
            ZTest LEqual
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"
            TEXTURE2D(_MatcapTex);
            SAMPLER(sampler_MatcapTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MatcapTex_ST;
                float4 _TintColor;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalVS : TEXCOORD0;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.normalVS = mul((float3x3)UNITY_MATRIX_V, normalWS);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float3 n = normalize(input.normalVS);
                float2 matcapUV = n.xy * 0.5 + 0.5;
                half4 matcapSample = SAMPLE_TEXTURE2D(_MatcapTex, sampler_MatcapTex, matcapUV);
                half3 base = matcapSample.rgb * _TintColor.rgb;
                half3 minTint = _TintColor.rgb * 0.65;
                return half4(max(base, minTint), 1);
            }
            ENDHLSL
        }
    }
    FallBack "Universal Render Pipeline/Unlit"
}
