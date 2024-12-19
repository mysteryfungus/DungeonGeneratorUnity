Shader "Custom/GridShader"
{
    Properties
    {
        _GridColor ("Grid Color", Color) = (1, 1, 1, 1)
        _BackgroundColor ("Background Color", Color) = (0, 0, 0, 1)
        _CellSize ("Cell Size", Float) = 1.0
        _LineWidth ("Line Width", Float) = 0.05
        _ShiftX ("Shift X", Float) = 0.0
        _ShiftY ("Shift Y", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Pass
        {
            ZWrite On
            ZTest LEqual
            Cull Off
            Lighting Off

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
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _GridColor;
            float4 _BackgroundColor;
            float _CellSize;
            float _LineWidth;
            float _ShiftX;
            float _ShiftY;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Привязываем координаты к ячейкам сетки с учётом сдвига
                float2 gridUV = (i.worldPos.xy + float2(_ShiftX, _ShiftY)) / _CellSize;

                // Определяем, насколько близко текущая точка к линии
                float lineX = abs(frac(gridUV.x) - 0.5);
                float lineY = abs(frac(gridUV.y) - 0.5);

                // Учитываем ширину линий
                float lineThickness = _LineWidth / _CellSize;

                // Если точка попадает в линию, используем цвет сетки, иначе фон
                float mask = step(lineThickness, min(lineX, lineY));
                return lerp(_GridColor, _BackgroundColor, mask);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
