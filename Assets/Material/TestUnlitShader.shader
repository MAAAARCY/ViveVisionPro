Shader "Unlit/TestUnlitShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        //_DistortionAmount ("Distortion Amount", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            /*
            float2 ModifyLensDistortion(float2 distortionUV)
            {
                float2 rVec = (distortionUV - float2(0.5f, 0.5f));
                rVec.x *= ASPECT_RATIO;

                
                return uv;
            }
            */
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 center = 0.25;
                float2 offset = i.uv - center;
                float distance = length(offset);
                float2 distortedUV = center + offset * distance * 0.1;
                
                fixed4 col = tex2D(_MainTex, distortedUV);
                //fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }

            ENDCG
        }
    }
}
