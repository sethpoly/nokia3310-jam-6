Shader "Unlit/1BitColorShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold ("Threshold", Range(0, 1)) = 0.5
        _Color1 ("Dark Color", Color) = (1, 0, 0, 1)
        _Color2 ("Light Color", Color) = (0, 1, 0, 1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

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
            float _Threshold;
            fixed4 _Color1;
            fixed4 _Color2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float grayscale = dot(col.rgb, float3(0.299, 0.587, 0.114));
                fixed4 finalColor;

                if (grayscale > _Threshold)
                    finalColor = _Color2; // Change to Color 2
                else
                    finalColor = _Color1; // Change to Color 1

                // Set alpha to either 0 or 1
                finalColor.a = (col.a > 0.5) ? 1.0 : 0.0;

                return finalColor;
            }
            ENDCG
        }
    }
}
