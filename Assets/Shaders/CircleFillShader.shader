Shader "Custom/CircleFillShader"
{
    Properties
    {
        _Color("Fill Color", Color) = (1, 1, 1, 1)
        _FillAmount("Fill Amount", Range(0, 1)) = 0
    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float _FillAmount;
            fixed4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.vertex.xy * 0.5 + 0.5;
                float angle = atan2(uv.y - 0.5, uv.x - 0.5);
                float normalizedAngle = (angle + 3.14159) / 6.28318; // Нормализуем угол от 0 до 1
                float alpha = smoothstep(_FillAmount - 0.01, _FillAmount, normalizedAngle);
                return fixed4(_Color.rgb, alpha * _Color.a);
            }
            ENDCG
        }
    }
}