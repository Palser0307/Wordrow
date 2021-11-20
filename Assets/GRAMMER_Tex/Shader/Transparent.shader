Shader "Unlit/Transparent"
{
    Properties
    {
        _Color("Color Property", Color)=(1,0,0,0.3)
    }
    SubShader
    {
        Tags  { "Queue"="Transparent"
                "RenderType"= "Transparent"
            }
        Blend SrcAlpha OneMinusSrcAlpha
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 _Color;
            
            fixed4 frag (v2f i) : SV_Target
            {
               
                return _Color;
            }
            ENDCG
        }
    }
}
