﻿Shader "Sprite/PalleteSwapByRed"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SwapTex("Color Data", 2D) = "black" {}
        _Color("Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off 
        Blend One OneMinusSrcAlpha
        ZWrite Off ZTest Always

        Tags { "Queue" = "Transparent" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _AlphaTex;
            sampler2D _SwapTex;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, i.uv);
                fixed4 swapCol = tex2D(_SwapTex, float2(c.r, 0));
                fixed4 final = lerp(c, swapCol, swapCol.a) * _Color;
                final.a = c.a;
                final.rgb *= c.a;
                return final;
            }
            ENDCG
        }
    }
}
