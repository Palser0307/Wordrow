Shader "Unlit/Plane_Magic"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [MaterialToggle] _Enable ("Enable Texture", Int) = 0
    }
    SubShader
    {
        Tags  { "Queue"="Transparent"
                "RenderType"= "Transparent"
            }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100
        Cull off

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            int _Enable;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                i.uv-=float2(0.5,0.5);
                i.uv=float2(_CosTime.x*i.uv.x-_SinTime.x*i.uv.y,_SinTime.x*i.uv.x+_CosTime.x*i.uv.y);

                i.uv+=float2(0.5,0.5);

                fixed4 col = tex2D(_MainTex, i.uv)*_Enable;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
